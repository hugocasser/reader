using Application.Dtos;
using Application.Dtos.Views;
using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IUsersRepository
{
    public Task AddUser(User user);
    public Task DeleteUser(User user);
    public Task UpdateUser(User user);
    public Task<User?> GetUserByIdAsync(Guid userId);
    public Task<IEnumerable<User>> GetAllUsers(PageSettings pageSettings);
}