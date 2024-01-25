using BusinessLogicLayer.Abstractions.Dtos;

namespace BusinessLogicLayer.Abstractions.Services.DataServices;

public interface IUsersService
{
    Task RegisterUserAsync(RegisterUserRequestDto request, CancellationToken cancellationToken);
    Task<IEnumerable<ViewUserRequestDto>> GetAllUsersAsync(CancellationToken cancellationToken);
    Task<ViewUserRequestDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateUserAsync(UpdateUserRequestDto userRequestViewDto, CancellationToken cancellationToken);
    Task<AuthTokens> LoginUserAsync(LoginUserRequestDto loginUserRequestDto, CancellationToken cancellationToken);
    Task<string> GiveRoleToUserAsync(GiveRoleToUserRequestDto giveRoleToUserRequestDto, CancellationToken cancellationToken);
}