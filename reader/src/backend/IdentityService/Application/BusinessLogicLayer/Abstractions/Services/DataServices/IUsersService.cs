using BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;
using BusinessLogicLayer.Abstractions.Dtos.ViewDtos;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.Abstractions.Services.DataServices;

public interface IUsersService
{
    Task<IEnumerable<ViewUserDto>> GetAllUsersAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task RegisterUserAsync(RegisterUserRequestDto request, CancellationToken cancellationToken);
    Task<ViewUserDto> GetUserByIdAsync(Guid id);
    Task DeleteUserByIdAsync(Guid id);
    Task UpdateUserAsync(UpdateUserRequestDto updateUserRequest, CancellationToken cancellationToken);
    Task<AuthTokens> LoginUserAsync(LoginUserRequestDto loginUserRequestDto, CancellationToken cancellationToken);
    Task<string> GiveRoleToUserAsync(GiveRoleToUserRequestDto giveRoleToUserRequestDto);
    Task<IdentityResult> ConfirmUserEmail(Guid id, string code);
    Task<IdentityResult> ResendEmailConfirmMessageAsync(string email, string password);
}