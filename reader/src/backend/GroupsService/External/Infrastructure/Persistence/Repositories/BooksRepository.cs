using Application.Abstractions.Repositories;
using Domain.Models;
using Infrastructure.Abstractions;

namespace Infrastructure.Persistence.Repositories;

public class BooksRepository(WriteDbContext _writeDbContext, ReadDbContext _readDbContext)
    : BaseRepository<Book>(_writeDbContext, _readDbContext),  IBooksRepository
{
    
}