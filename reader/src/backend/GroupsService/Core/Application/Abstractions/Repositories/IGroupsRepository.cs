using Application.Dtos;
using Application.Dtos.Views;
using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IGroupsRepository
{
    public Task CreateGroupAsync(Group group);
    public Task<Group?> GetGroupByIdAsync(Guid id);
    public Task<IEnumerable<Group>> GetGroupsAsync(PageSettings pageSettings);
    public Task <IEnumerable<Tuple<Note,User>>> GetGroupNotesAsync(Guid groupId, ReadingPageSettings pageSettings);
    public Task UpdateGroupAsync(Group group);
    public Task DeleteGroupByIdAsync(Guid groupId);
    public 
    public Task SaveChangesAsync();
}