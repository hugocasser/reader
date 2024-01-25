using DataAccessLayer.Abstractions.Repositories;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Persistence.Repositories;

public class UserRolesRepository : IUserRolesRepository
{
    private readonly RoleManager<UserRole?> _roleManager;
    
    public UserRolesRepository(RoleManager<UserRole?> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<IdentityResult> CreateRoleAsync(string role)
    {
        return await _roleManager.CreateAsync(new UserRole(role));
    }

    public async Task<IdentityResult> DeleteRoleAsync(UserRole? role)
    {
        return await _roleManager.DeleteAsync(role);
    }

    public async Task<bool> RoleExistsAsync(string role)
    {
        return await _roleManager.RoleExistsAsync(role);
    }

    public async Task<UserRole?> GetRoleInfoAsync(string role)
    {
        return await _roleManager.FindByNameAsync(role);
    }
}