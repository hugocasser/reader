using Application.Abstractions.Repositories;
using Application.Abstractions.Services.Cache;
using Application.Common;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.BackgroundJobs;

public class BackgroundCacheService(ILogger<BackgroundCacheService> _logger, ICashedNotesService _cashedNotesService,
    INotesRepository _notesRepository)
{
    public async Task PushNotes()
    {
        _logger.LogInformation("--> Start pushing notes...");
        var cachedNotes = await _cashedNotesService.GetNotesAsync(100, default);

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