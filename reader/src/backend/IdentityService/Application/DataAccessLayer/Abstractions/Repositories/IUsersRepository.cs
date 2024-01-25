using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Abstractions.Repositories;

public interface IUsersRepository
{
    public IQueryable<User?> GetAllUsersAsync();
    public Task<User?> GetUserByIdAsync(Guid id);
    public Task<User?> GetUserByEmailAsync(string email);
    public Task<IEnumerable<string>> GetUserRolesAsync(User user);
    public Task<bool> IsUserExistAsync(Guid id);
    public Task<IdentityResult> CreateUserAsync(User user, string password);
    public Task<IdentityResult> UpdateUserAsync(User user);
    public Task<IdentityResult> DeleteUserAsync(User user);
    public Task<IdentityResult> SetUserRoleAsync(User user, string role);
    public Task<bool> CheckPasswordAsync(User user, string requestPassword);
    public Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);
    public Task<string> GeneratePasswordResetTokenAsync(User user);
    public Task<List<User>> ToListAsync(IQueryable<User> users);
    public Task<IdentityResult> ConfirmUserEmail(User user, string code);
}