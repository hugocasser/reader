using Application.Abstractions.Repositories;
using Application.Dtos.Views;
using Domain.Models;

namespace Infrastructure.Persistence.Repositories;

public class BooksRepository(WriteDbContext _writeDbContext, ReadDbContext _readDbContext)
    : BaseRepository<Book, BookViewDto>(_writeDbContext, _readDbContext),  IBooksRepository;