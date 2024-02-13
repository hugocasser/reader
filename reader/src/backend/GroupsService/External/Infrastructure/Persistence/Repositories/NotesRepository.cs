using Application.Abstractions.Repositories;
using Domain.Models;
using Infrastructure.Abstractions;

namespace Infrastructure.Persistence.Repositories;

public class NotesRepository(WriteDbContext _writeDbContext, ReadDbContext _readDbContext)
    : BaseRepository<Note>(_writeDbContext, _readDbContext), INotesRepository;