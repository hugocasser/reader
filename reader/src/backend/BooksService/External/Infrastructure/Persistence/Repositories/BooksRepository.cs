using Application.Abstractions.Repositories;
using Application.Dtos.Requests;
using Domain.Models;
using Infrastructure.Abstractions;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Repositories;

public class BooksRepository(MongoDbContext _dbContext) : BaseRepository<Book>(_dbContext.BooksCollection), IBooksRepository
{
    public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(Guid categoryId, PageSettingRequestDto pageSettingRequestDto,
        CancellationToken cancellationToken)
    {
        return await _dbContext.BooksCollection
            .Find(book => book.CategoryId == categoryId).Skip(pageSettingRequestDto.Skip())
            .Limit(pageSettingRequestDto.PageSize).ToListAsync(cancellationToken);
    }
}