using BusinessLogicLayer.Abstractions.Dtos;
using BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;
using BusinessLogicLayer.Abstractions.Dtos.ViewDtos;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.Abstractions.Services.DataServices;

public interface IUsersService
{
    Task RegisterUserAsync(RegisterUserRequestDto request);
    Task<IEnumerable<ViewUserDto>> GetAllUsersAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task<ViewUserDto> GetUserByIdAsync(Guid id);
    Task DeleteUserByIdAsync(Guid id);
    Task UpdateUserAsync(UpdateUserRequestDto userRequestViewDto);
    Task<AuthTokens> LoginUserAsync(LoginUserRequestDto loginUserRequestDto, CancellationToken cancellationToken);
    Task<string> GiveRoleToUserAsync(GiveRoleToUserRequestDto giveRoleToUserRequestDto);
    Task<IdentityResult> ConfirmUserEmail(Guid id, string code);
    Task<IdentityResult> ResendEmailConfirmMessageAsync(string email, string password);
}