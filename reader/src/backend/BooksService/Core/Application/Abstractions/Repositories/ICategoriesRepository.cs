using Application.Dtos.Requests;
using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface ICategoriesRepository : IBaseRepository<Category>
{
    public Task<Category> GetCategoryByNameAsync(string name, CancellationToken cancellationToken);
}