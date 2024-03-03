using BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;
using BusinessLogicLayer.Abstractions.Dtos.ViewDtos;
using BusinessLogicLayer.Abstractions.Services.AuthServices;
using BusinessLogicLayer.Abstractions.Services.DataServices;
using BusinessLogicLayer.Abstractions.Services.Grpc;
using BusinessLogicLayer.Common;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Options;
using DataAccessLayer.Abstractions.Repositories;
using DataAccessLayer.Models;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BusinessLogicLayer.Services.DataServices;

public class UsersService(
    UserManager<User> _usersManager,
    IAuthTokenGeneratorService _authService,
    IRefreshTokenGeneratorService _refreshTokenService,
    IRefreshTokensRepository _refreshTokensRepository,
    IEmailConfirmMessageService _emailConfirmMessageService,
    IGrpcUserService _grpcUserService)
    : IUsersService
{
    public async Task RegisterUserAsync(RegisterUserRequestDto request, CancellationToken cancellationToken)
    {
        var user = request.ToUser();
        var result = await _usersManager.CreateAsync(user, request.Password);
        
        Utilities.AggregateIdentityErrorsAndThrow(result);
        Utilities.AggregateIdentityErrorsAndThrow(await _usersManager.AddToRoleAsync(user, EnumRoles.User.ToString()));
        
        await _grpcUserService.SendUserCreatedAsync(user);
        await _emailConfirmMessageService.SendEmailConfirmMessageAsync(user);
        await _emailConfirmMessageService.SendEmailConfirmMessageAsync(user);
    }

    public async Task<IEnumerable<ViewUserDto>> GetAllUsersAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var users = await _usersManager.GetUsers(page, pageSize).ToListAsync(cancellationToken);
        var viewUserDtos = new List<ViewUserDto>();
        
        foreach (var user in users)
        {
            viewUserDtos.Add(ViewUserDto.MapFromModel(user, await _usersManager.GetRolesAsync(user)));
        }
        
        return viewUserDtos;
    }

    public async Task<ViewUserDto> GetUserByIdAsync(Guid id)
    {
        var user = await _usersManager.FindByIdAsync(id.ToString());

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        return ViewUserDto.MapFromModel(user, await _usersManager.GetRolesAsync(user));
    }

    public async Task DeleteUserByIdAsync(Guid id)
    {
        var user = await _usersManager.FindByIdAsync(id.ToString());

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }
        
        var result = await _usersManager.DeleteAsync(user);
        Utilities.AggregateIdentityErrorsAndThrow(result);
        
        await _grpcUserService.SendUserDeletedAsync(user);
    }
    public async Task UpdateUserAsync(UpdateUserRequestDto updateUserRequest, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(updateUserRequest.NewEmail) && string.IsNullOrEmpty(updateUserRequest.FirstName)
                                                       && string.IsNullOrEmpty(updateUserRequest.LastName))
        {
            throw new BadRequestException("Nothing to update");
        }

        var user = await _usersManager.FindByEmailAsync(updateUserRequest.OldEmail);

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }
        
        updateUserRequest.UpdateUser(user);
        
        var result = await _usersManager.UpdateAsync(user);
        Utilities.AggregateIdentityErrorsAndThrow(result);
        
        await _grpcUserService.SendUserUpdatedAsync(user);
    }

    public async Task<AuthTokens> LoginUserAsync(LoginUserRequestDto loginUserRequestDto, CancellationToken cancellationToken)
    {
        var user = await GetUserByEmail(loginUserRequestDto);
        var userRoles = await _usersManager.GetRolesAsync(user);
        
        var userToken = _authService.GenerateToken(user.Id, user.Email, userRoles);
        var refreshToken = _refreshTokenService.GenerateToken(user.Id);
        
        await _refreshTokensRepository.AddAsync(refreshToken, cancellationToken);
        await _refreshTokensRepository.SaveChangesAsync(cancellationToken);

        return new AuthTokens(user.Id, userToken, refreshToken.Token);
    }

    public async Task<string> GiveRoleToUserAsync(GiveRoleToUserRequestDto giveRoleToUserRequestDto)
    {
        var user = await _usersManager.FindByIdAsync(giveRoleToUserRequestDto.Id.ToString());

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }
        
        var result = await _usersManager.AddToRoleAsync(user, "User");
        Utilities.AggregateIdentityErrorsAndThrow(result);
        
        var userRoles = await _usersManager.GetRolesAsync(user);
        
        return _authService.GenerateToken(user.Id, user.Email, userRoles);
    }

    public async Task<IdentityResult> ConfirmUserEmail(Guid id, string code)
    {
        var user = await _usersManager.FindByIdAsync(id.ToString());

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

         var result = await _usersManager.ConfirmEmailAsync(user, code);
        
        Utilities.AggregateIdentityErrorsAndThrow(result);

        return result;
    }

    public async Task<IdentityResult> ResendEmailConfirmMessageAsync(string email, string password)
    {
        var user = await _usersManager.FindByEmailAsync(email);
        if (user is null || !await _usersManager.CheckPasswordAsync(user, password))
        {
            throw new IncorrectEmailOrPasswordException();
        }
        
        if (user.EmailConfirmed)
        {
            throw new IdentityException("The Email already confirmed");
        }
        
        await _emailConfirmMessageService.SendEmailConfirmMessageAsync(user);
        
        return IdentityResult.Success;
    }

    private async Task<User?> GetUserByEmail(LoginUserRequestDto request)
    {
        var user = await _usersManager.FindByEmailAsync(request.Email);
        
        if (user is null || !await _usersManager.CheckPasswordAsync(user, request.Password))
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