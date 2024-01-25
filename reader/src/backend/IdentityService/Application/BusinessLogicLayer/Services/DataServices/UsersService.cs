using BusinessLogicLayer.Abstractions.Dtos;
using BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;
using BusinessLogicLayer.Abstractions.Services;
using BusinessLogicLayer.Abstractions.Services.DataServices;
using BusinessLogicLayer.Common;
using BusinessLogicLayer.Exceptions;
using DataAccessLayer.Abstractions.Repositories;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Services.DataServices;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IAuthTokenGeneratorService _authService;
    private readonly IRefreshTokenGeneratorService _refreshTokenService;
    private readonly IRefreshTokensRepository _refreshTokensRepository;
    

    public UsersService(IUsersRepository usersRepository, IAuthTokenGeneratorService authService,
        IRefreshTokenGeneratorService refreshTokenService, IRefreshTokensRepository refreshTokensRepository)
    {
        _usersRepository = usersRepository;
        _authService = authService;
        _refreshTokenService = refreshTokenService;
        _refreshTokensRepository = refreshTokensRepository;
    }

    public async Task RegisterUserAsync(RegisterUserRequestDto request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            FirstName = string.IsNullOrEmpty(request.FirstName) ? string.Empty : request.FirstName,
            LastName = string.IsNullOrEmpty(request.LastName) ? string.Empty : request.LastName,
            UserName = request.Username
        };
        var result = await _usersRepository.CreateUserAsync(user, request.Password);

        Utilities.AggregateIdentityErrorsAndThrow(result);
        Utilities.AggregateIdentityErrorsAndThrow(await _usersRepository.SetUserRoleAsync(user, "User"));
    }

    public async Task<IEnumerable<ViewUserDto>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        var users = await _usersRepository.GetAllUsersAsync().ToListAsync(cancellationToken: cancellationToken);
        var viewUserDtos = new List<ViewUserDto>();
        foreach (var user in users)
        {
            viewUserDtos.Add(ViewUserDto.MapFromModel(user, await _usersRepository.GetUserRolesAsync(user)));
        }

        return viewUserDtos;
    }

    public async Task<ViewUserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserByIdAsync(id);

        if (user is null)
        {
            throw new NotFoundExceptionWithStatusCode("User not found");
        }

        return ViewUserDto.MapFromModel(user, await _usersRepository.GetUserRolesAsync(user));
    }

    public async Task DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserByIdAsync(id);

        if (user is null)
        {
            throw new NotFoundExceptionWithStatusCode("User not found");
        }
        
        var result = await _usersRepository.DeleteUserAsync(user);
        Utilities.AggregateIdentityErrorsAndThrow(result);
    }

    public async Task UpdateUserAsync(UpdateUserRequestDto userRequestUserRequestDto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(userRequestUserRequestDto.NewEmail) && string.IsNullOrEmpty(userRequestUserRequestDto.FirstName)
                                                       && string.IsNullOrEmpty(userRequestUserRequestDto.LastName))
        {
            throw new BadRequestExceptionWithStatusCode("Nothing to update");
        }
        
        var user = await _usersRepository.GetUserByEmailAsync(userRequestUserRequestDto.OldEmail);

        if (user is null)
        {
            throw new NotFoundExceptionWithStatusCode("User not found");
        }
        
        user.FirstName = string.IsNullOrEmpty(userRequestUserRequestDto.FirstName) ? user.FirstName : userRequestUserRequestDto.FirstName;
        user.LastName = string.IsNullOrEmpty(userRequestUserRequestDto.LastName) ? user.LastName : userRequestUserRequestDto.LastName;
        user.Email = string.IsNullOrEmpty(userRequestUserRequestDto.NewEmail) ? user.Email : userRequestUserRequestDto.NewEmail;
        await _usersRepository.UpdateUserAsync(user);
    }

    public async Task<AuthTokens> LoginUserAsync(LoginUserRequestDto loginUserRequestDto, CancellationToken cancellationToken)
    {
        var user = await GetUserByEmail(loginUserRequestDto);
        var userRoles = await _usersRepository.GetUserRolesAsync(user);
        
        var userToken = _authService.GenerateToken(user.Id, user.Email, userRoles);
        var refreshToken = _refreshTokenService.GenerateToken(user.Id);
        
        await _refreshTokensRepository.AddAsync(refreshToken, cancellationToken);
        await _refreshTokensRepository.SaveChangesAsync(cancellationToken);

        return new AuthTokens(user.Id, userToken, refreshToken.Token);
    }

    public async Task<string> GiveRoleToUserAsync(GiveRoleToUserRequestDto giveRoleToUserRequestDto, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserByIdAsync(giveRoleToUserRequestDto.Id);

        if (user is null)
        {
            throw new NotFoundExceptionWithStatusCode("User not found");
        }

        await _usersRepository.SetUserRoleAsync(user, giveRoleToUserRequestDto.Role.Name);
        
        var userRoles = await _usersRepository.GetUserRolesAsync(user);
        
        return _authService.GenerateToken(user.Id, user.Email, userRoles);
    }

    private async Task<User?> GetUserByEmail(LoginUserRequestDto request)
    {
        var user = await _usersRepository.GetUserByEmailAsync(request.Email);
        
        if (user is null || !await _usersRepository.CheckPasswordAsync(user, request.Password))
        {
            throw new IncorrectEmailOrPasswordExceptionWithStatusCode();
        }
        
        //if (!user.EmailConfirmed)
        //{
            //throw new EmailNotConfirmedExceptionWithStatusCode();
        //}

        return user;
    }
}