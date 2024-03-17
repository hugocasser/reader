using Application.Dtos.Requests;
using Application.Dtos.Views;
using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IGroupsRepository : IBaseRepository<Group, GroupViewDto>
{
    Task CreateGroupAsync(Group group, CancellationToken cancellationToken);
    Task CreateGroupAsyncInReadDbContextAsync(Group notificationEntity, CancellationToken cancellationToken);
}