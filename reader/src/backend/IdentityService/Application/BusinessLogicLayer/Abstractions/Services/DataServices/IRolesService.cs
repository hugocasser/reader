using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.Abstractions.Services.DataServices;

public interface IRolesService
{
    public Task<IdentityResult> CreateRoleAsync(string role);
    public Task<IdentityResult> DeleteRoleAsync(string role);
    public Task<UserRole> GetRoleInfoAsync(string role);
}