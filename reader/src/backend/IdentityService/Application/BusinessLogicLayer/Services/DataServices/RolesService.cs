using BusinessLogicLayer.Abstractions.Services.DataServices;
using BusinessLogicLayer.Exceptions;
using DataAccessLayer.Abstractions.Repositories;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.Services.DataServices;

public class RolesService(RoleManager<UserRole> rolesManager) : IRolesService
{
    public async Task<IdentityResult> CreateRoleAsync(string? role)
    {
        if (string.IsNullOrEmpty(role))
        {
            throw new BadRequestException("Role cannot be null or empty");
        }

        return await rolesManager.CreateAsync(new UserRole()
        {
            Id = Guid.NewGuid(),
            Name = role,
        });
    }
    

    public async Task<IdentityResult> DeleteRoleAsync(string role)
    {
        if (string.IsNullOrEmpty(role))
        {
            throw new BadRequestException("Role cannot be null or empty");
        }
        
        var roleToDelete = await rolesManager.FindByNameAsync(role);

        if (roleToDelete == null)
        {
            throw new NotFoundException("Role not found");    
        }

        return await rolesManager.DeleteAsync(roleToDelete);
    }

    public async Task<bool> RoleExistsAsync(string? role)
    {
        if (string.IsNullOrEmpty(role))
        {
            throw new BadRequestException("Role cannot be null or empty");
        }
        
        return await rolesManager.RoleExistsAsync(role);
    }

    public async Task<UserRole> GetRoleInfoAsync(string role)
    {
        var roleInfo = await rolesManager.FindByNameAsync(role);

        if (roleInfo is null)
        {
            throw new NotFoundException("Role not found");
        }
        
        return roleInfo;
    }
}