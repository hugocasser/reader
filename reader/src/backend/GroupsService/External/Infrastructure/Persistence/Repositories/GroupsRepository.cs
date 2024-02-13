using Application.Abstractions.Repositories;
using Application.Dtos.Requests;
using Domain.Models;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class GroupsRepository(WriteDbContext _writeDbContext, ReadDbContext _readDbContext)
    : BaseRepository<Group>(_writeDbContext, _readDbContext), IGroupsRepository
{
    public async Task<IEnumerable<Tuple<Note, User>>> GetGroupNotesAsync
        (Guid groupId, ReadingPageSettingsRequestDto pageSettingsRequestDto, CancellationToken cancellationToken)
    {
        var progresses = await _readDbContext.UserBookProgresses
            .Where(progress => progress.GroupId == groupId)
            .Include(userBookProgress => userBookProgress.UserNotes)
            .Include(userBookProgress => userBookProgress.User)
            .ToListAsync(cancellationToken);

        var notes = new List<Tuple<Note, User>>();

        foreach (var progress in progresses )
        {
            notes.AddRange(progress.UserNotes
                .Where(note => note.NotePosition <= pageSettingsRequestDto.LastNotePosition &&
                    note.NotePosition >= pageSettingsRequestDto.FirstNotePosition)
                .Select(note => new Tuple<Note, User?>(note, progress.User)));
        }

        return notes;
    }
}