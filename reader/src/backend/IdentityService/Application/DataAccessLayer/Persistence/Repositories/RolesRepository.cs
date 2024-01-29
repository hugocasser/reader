using DataAccessLayer.Abstractions.Repositories;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Persistence.Repositories;

public class UserRolesRepository(RoleManager<UserRole?> roleManager) : IUserRolesRepository
{
    public async Task<IdentityResult> CreateRoleAsync(string role)
    {
        return await roleManager.CreateAsync(new UserRole(role));
    }

    public async Task<IdentityResult> DeleteRoleAsync(UserRole? role)
    {
        return await roleManager.DeleteAsync(role);
    }

    public async Task<bool> RoleExistsAsync(string role)
    {
        return await roleManager.RoleExistsAsync(role);
    }

    public async Task<UserRole?> GetRoleInfoAsync(string role)
    {
        return await roleManager.FindByNameAsync(role);
    }
}