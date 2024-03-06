using Application.Dtos.Views;
using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface INotesRepository : IBaseRepository<Note, NoteViewDto>;