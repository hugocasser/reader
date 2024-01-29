using DataAccessLayer.Abstractions.Repositories;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Persistence.Repositories;

public class UsersRepository(UserManager<User> userManager) : IUsersRepository
{
    public IQueryable<User?> GetAllUsersAsync()
    {
        return userManager.Users;
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await userManager.FindByIdAsync(id.ToString());
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await userManager.FindByEmailAsync(email);
    }

    public async Task<IEnumerable<string>> GetUserRolesAsync(User user)
    {
        return await userManager.GetRolesAsync(user);
    }

    public async Task<bool> IsUserExistAsync(Guid id)
    {
        return await userManager.FindByIdAsync(id.ToString()) is not null;
    }

    public async Task<IdentityResult> CreateUserAsync(User user, string password)
    {
        return await userManager.CreateAsync(user, password);
    }

    public async Task<IdentityResult> UpdateUserAsync(User user)
    {
        return await userManager.UpdateAsync(user); 
    }

    public async Task<IdentityResult> DeleteUserAsync(User user)
    {
        return await userManager.DeleteAsync(user);
    }

    public async Task<IdentityResult> SetUserRoleAsync(User user, string role)
    {
        return await userManager.AddToRoleAsync(user, role);
    }

    public async Task<bool> CheckPasswordAsync(User user, string requestPassword)
    {
        return await userManager.CheckPasswordAsync(user, requestPassword);
    }

    public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
    {
        return await userManager.ResetPasswordAsync(user, token, newPassword);
    }

    public async Task<string> GeneratePasswordResetTokenAsync(User user)
    {
        return await userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<List<User>> ToListAsync(IQueryable<User> users)
    {
        return await users.ToListAsync();
    }

    public async Task<IdentityResult> ConfirmUserEmail(User user, string code)
    {
        return await userManager.ConfirmEmailAsync(user, code);
    }
}