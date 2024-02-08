using BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;
using BusinessLogicLayer.Abstractions.Dtos.ViewDtos;
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
    UserManager<User> usersManager,
    IAuthTokenGeneratorService authService,
    IRefreshTokenGeneratorService refreshTokenService,
    IRefreshTokensRepository refreshTokensRepository,
    IEmailConfirmMessageService emailConfirmMessageService)
    : IUsersService
{
    public async Task RegisterUserAsync(RegisterUserRequestDto request, CancellationToken cancellationToken)
    {
        var user = request.ToUser();
        var result = await usersManager.CreateAsync(user, request.Password);
        
        Utilities.AggregateIdentityErrorsAndThrow(result);
        Utilities.AggregateIdentityErrorsAndThrow(await usersManager.AddToRoleAsync(user, EnumRoles.User.ToString()));
        
        await emailConfirmMessageService.SendEmailConfirmMessageAsync(user);
    }

    public async Task<IEnumerable<ViewUserDto>> GetAllUsersAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var users = await usersManager.GetUsers(page, pageSize).ToListAsync(cancellationToken);
        var viewUserDtos = new List<ViewUserDto>();
        
        foreach (var user in users)
        {
            viewUserDtos.Add(ViewUserDto.MapFromModel(user, await usersManager.GetRolesAsync(user)));
        }

        return viewUserDtos;
    }

    public async Task<ViewUserDto> GetUserByIdAsync(Guid id)
    {
        var user = await usersManager.FindByIdAsync(id.ToString());

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        return ViewUserDto.MapFromModel(user, await usersManager.GetRolesAsync(user));
    }

    public async Task DeleteUserByIdAsync(Guid id)
    {
        var user = await usersManager.FindByIdAsync(id.ToString());

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }
        
        var result = await usersManager.DeleteAsync(user);
        Utilities.AggregateIdentityErrorsAndThrow(result);
    }
    public async Task UpdateUserAsync(UpdateUserRequestDto updateUserRequest, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(updateUserRequest.NewEmail) && string.IsNullOrEmpty(updateUserRequest.FirstName)
                                                       && string.IsNullOrEmpty(updateUserRequest.LastName))
        {
            throw new BadRequestException("Nothing to update");
        }

        var user = await usersManager.FindByEmailAsync(updateUserRequest.OldEmail);

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }
        
        updateUserRequest.UpdateUser(user);
        
        var result = await usersManager.UpdateAsync(user);
        Utilities.AggregateIdentityErrorsAndThrow(result);
    }

    public async Task<AuthTokens> LoginUserAsync(LoginUserRequestDto loginUserRequestDto, CancellationToken cancellationToken)
    {
        var user = await GetUserByEmail(loginUserRequestDto);
        var userRoles = await usersManager.GetRolesAsync(user);
        
        var userToken = authService.GenerateToken(user.Id, user.Email, userRoles);
        var refreshToken = refreshTokenService.GenerateToken(user.Id);
        
        await refreshTokensRepository.AddAsync(refreshToken, cancellationToken);
        await refreshTokensRepository.SaveChangesAsync(cancellationToken);

        return new AuthTokens(user.Id, userToken, refreshToken.Token);
    }

    public async Task<string> GiveRoleToUserAsync(GiveRoleToUserRequestDto giveRoleToUserRequestDto)
    {
        var user = await usersManager.FindByIdAsync(giveRoleToUserRequestDto.Id.ToString());

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }
        
        var result = await usersManager.AddToRoleAsync(user, "User");
        Utilities.AggregateIdentityErrorsAndThrow(result);
        
        var userRoles = await usersManager.GetRolesAsync(user);
        
        return authService.GenerateToken(user.Id, user.Email, userRoles);
    }

    public async Task<IdentityResult> ConfirmUserEmail(Guid id, string code)
    {
        var user = await usersManager.FindByIdAsync(id.ToString());

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        var result = await usersManager.ConfirmEmailAsync(user, code);
        
        Utilities.AggregateIdentityErrorsAndThrow(result);

        return result;
    }

    public async Task<IdentityResult> ResendEmailConfirmMessageAsync(string email, string password)
    {
        var user = await usersManager.FindByEmailAsync(email);
        if (user is null || !await usersManager.CheckPasswordAsync(user, password))
        {
            throw new IncorrectEmailOrPasswordException();
        }
        
        if (user.EmailConfirmed)
        {
            throw new IdentityException("The Email already confirmed");
        }
        
        await emailConfirmMessageService.SendEmailConfirmMessageAsync(user);
        
        return IdentityResult.Success;
    }

    private async Task<User?> GetUserByEmail(LoginUserRequestDto request)
    {
        var user = await usersManager.FindByEmailAsync(request.Email);
        
        if (user is null || !await usersManager.CheckPasswordAsync(user, request.Password))
        {
            throw new IncorrectEmailOrPasswordException();
        }
        
        if (!user.EmailConfirmed)
        {
            throw new EmailNotConfirmedException();
        }

        return user;
    }
}