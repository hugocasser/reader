using BusinessLogicLayer.Abstractions.Configurations;
using BusinessLogicLayer.Abstractions.Dtos;
using BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;
using BusinessLogicLayer.Abstractions.Services;
using BusinessLogicLayer.Abstractions.Services.AuthServices;
using BusinessLogicLayer.Abstractions.Services.DataServices;
using BusinessLogicLayer.Common;
using BusinessLogicLayer.Exceptions;
using DataAccessLayer.Abstractions.Repositories;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Services.DataServices;

public class UsersService(
    IUsersRepository usersRepository,
    IAuthTokenGeneratorService authService,
    IRefreshTokenGeneratorService refreshTokenService,
    IRefreshTokensRepository refreshTokensRepository,
    IEmailConfirmMessageService emailConfirmMessageService,
    UserManager<User> usersManager)
    : IUsersService
{
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
        var result = await usersRepository.CreateUserAsync(user, request.Password);
        
        Utilities.AggregateIdentityErrorsAndThrow(result);
        Utilities.AggregateIdentityErrorsAndThrow(await usersRepository.SetUserRoleAsync(user, "User"));
        
        await emailConfirmMessageService.SendEmailConfirmMessageAsync(user);
    }

    public async Task<IEnumerable<ViewUserDto>> GetAllUsersAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var users = await usersManager.Users.AsNoTracking().OrderBy(user => user.Email).Skip((page-1)*pageSize).Take(pageSize)
            .Skip((page-1)*pageSize).Take(pageSize).ToListAsync(cancellationToken);
        var viewUserDtos = new List<ViewUserDto>();
        foreach (var user in users)
        {
            viewUserDtos.Add(ViewUserDto.MapFromModel(user, await usersRepository.GetUserRolesAsync(user)));
        }

        return viewUserDtos;
    }

    public async Task<ViewUserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdAsync(id);

        if (user is null)
        {
            throw new NotFoundExceptionWithStatusCode("User not found");
        }

        return ViewUserDto.MapFromModel(user, await usersRepository.GetUserRolesAsync(user));
    }

    public async Task DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdAsync(id);

        if (user is null)
        {
            throw new NotFoundExceptionWithStatusCode("User not found");
        }
        
        var result = await usersRepository.DeleteUserAsync(user);
        Utilities.AggregateIdentityErrorsAndThrow(result);
    }

    public async Task UpdateUserAsync(UpdateUserRequestDto userRequestUserRequestDto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(userRequestUserRequestDto.NewEmail) && string.IsNullOrEmpty(userRequestUserRequestDto.FirstName)
                                                       && string.IsNullOrEmpty(userRequestUserRequestDto.LastName))
        {
            throw new BadRequestExceptionWithStatusCode("Nothing to update");
        }
        
        var user = await usersRepository.GetUserByEmailAsync(userRequestUserRequestDto.OldEmail);

        if (user is null)
        {
            throw new NotFoundExceptionWithStatusCode("User not found");
        }
        
        user.FirstName = string.IsNullOrEmpty(userRequestUserRequestDto.FirstName) ? user.FirstName : userRequestUserRequestDto.FirstName;
        user.LastName = string.IsNullOrEmpty(userRequestUserRequestDto.LastName) ? user.LastName : userRequestUserRequestDto.LastName;
        user.Email = string.IsNullOrEmpty(userRequestUserRequestDto.NewEmail) ? user.Email : userRequestUserRequestDto.NewEmail;
        await usersRepository.UpdateUserAsync(user);
    }

    public async Task<AuthTokens> LoginUserAsync(LoginUserRequestDto loginUserRequestDto, CancellationToken cancellationToken)
    {
        var user = await GetUserByEmail(loginUserRequestDto);
        var userRoles = await usersRepository.GetUserRolesAsync(user);
        
        var userToken = authService.GenerateToken(user.Id, user.Email, userRoles);
        var refreshToken = refreshTokenService.GenerateToken(user.Id);
        
        await refreshTokensRepository.AddAsync(refreshToken, cancellationToken);
        await refreshTokensRepository.SaveChangesAsync(cancellationToken);

        return new AuthTokens(user.Id, userToken, refreshToken.Token);
    }

    public async Task<string> GiveRoleToUserAsync(GiveRoleToUserRequestDto giveRoleToUserRequestDto, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdAsync(giveRoleToUserRequestDto.Id);

        if (user is null)
        {
            throw new NotFoundExceptionWithStatusCode("User not found");
        }

        await usersRepository.SetUserRoleAsync(user, giveRoleToUserRequestDto.Role.Name);
        
        var userRoles = await usersRepository.GetUserRolesAsync(user);
        
        return authService.GenerateToken(user.Id, user.Email, userRoles);
    }

    public async Task<IdentityResult> ConfirmUserEmail(Guid id, string code)
    {
        var user = await usersRepository.GetUserByIdAsync(id);

        if (user is null)
        {
            throw new NotFoundExceptionWithStatusCode("User not found");
        }
        
        var result = await usersRepository.ConfirmUserEmail(user, code);
        
        Utilities.AggregateIdentityErrorsAndThrow(result);

        return result;
    }

    public async Task<IdentityResult> ResendEmailConfirmMessageAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByEmailAsync(email);
        if (user is null || !await usersRepository.CheckPasswordAsync(user, password)) 
        { 
            throw new IncorrectEmailOrPasswordExceptionWithStatusCode();
        }
        
        if (user.EmailConfirmed)
        {
            throw new IdentityExceptionWithStatusCode("The Email already confirmed");
        }
        
        await emailConfirmMessageService.SendEmailConfirmMessageAsync(user);
        
        return IdentityResult.Success;
    }

    private async Task<User?> GetUserByEmail(LoginUserRequestDto request)
    {
        var user = await usersRepository.GetUserByEmailAsync(request.Email);
        
        if (user is null || !await usersRepository.CheckPasswordAsync(user, request.Password))
        {
            throw new IncorrectEmailOrPasswordExceptionWithStatusCode();
        }
        
        if (!user.EmailConfirmed)
        {
            throw new EmailNotConfirmedExceptionWithStatusCode();
        }

        return user;
    }
}