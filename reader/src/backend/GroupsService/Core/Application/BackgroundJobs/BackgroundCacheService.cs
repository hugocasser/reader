using Application.Abstractions.Repositories;
using Application.Abstractions.Services.Cache;
using Domain.Models;
using Infrastructure.Common;
using Microsoft.Extensions.Logging;

namespace Application.BackgroundJobs;

public class BackgroundCacheService(ILogger<BackgroundCacheService> _logger, IRedisCacheService _redisCacheService,
    INotesRepository _notesRepository)
{
    public async Task PushNotes()
    {
        _logger.LogInformation("--> Start pushing notes...");
        var cachedNotes = _redisCacheService.GetSetAsync<Note>(CachingKeys.Notes).ToBlockingEnumerable();

        foreach (var cachedNote in cachedNotes)
        {
            var note = new Note();
            note.CreateNote(cachedNote.NotePosition, cachedNote.UserBookProgress, cachedNote.Text);
            
            await _notesRepository.CreateAsync(cachedNote, default);
        }

        await _notesRepository.SaveChangesAsync(default);
        
        _logger.LogInformation("--> End pushing notes...");
    }
}