using Application.Dtos.Requests;
using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IGroupsRepository : IBaseRepository<Group>
{
    public Task<IEnumerable<Tuple<Note, User>>> GetGroupNotesAsync(Guid groupId,
        ReadingPageSettingsRequestDto pageSettingsRequestDto, CancellationToken cancellationToken);
}