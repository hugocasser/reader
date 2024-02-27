using Application.Abstractions.Repositories;
using Application.Dtos.Views;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class NotesRepository(WriteDbContext _writeDbContext, ReadDbContext _readDbContext)
    : BaseRepository<Note, NoteViewDto>(_writeDbContext, _readDbContext), INotesRepository;