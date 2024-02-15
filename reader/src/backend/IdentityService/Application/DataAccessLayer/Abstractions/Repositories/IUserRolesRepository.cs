using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Abstractions.Repositories;

public interface IUserRolesRepository
{
    public Task<IdentityResult> CreateRoleAsync(string role);
    public Task<IdentityResult> DeleteRoleAsync(UserRole? role);
    public Task<bool> RoleExistsAsync(string role);
    
    public Task<UserRole?> GetRoleInfoAsync(string role);
}