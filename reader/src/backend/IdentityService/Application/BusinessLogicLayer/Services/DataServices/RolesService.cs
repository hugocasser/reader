using BusinessLogicLayer.Abstractions.Services.DataServices;
using BusinessLogicLayer.Exceptions;
using DataAccessLayer.Abstractions.Repositories;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.Services.DataServices;

public class RolesService(IUserRolesRepository rolesRepository) : IRolesService
{
    public async Task<IdentityResult> CreateRoleAsync(string? role)
    {
        if (string.IsNullOrEmpty(role))
        {
            throw new BadRequestExceptionWithStatusCode("Role cannot be null or empty");
        }
        
        return await rolesRepository.CreateRoleAsync(role);
    }
    

    public async Task<IdentityResult> DeleteRoleAsync(string role)
    {
        if (string.IsNullOrEmpty(role))
        {
            throw new BadRequestExceptionWithStatusCode("Role cannot be null or empty");
        }
        
        var roleToDelete = await rolesRepository.GetRoleInfoAsync(role);

        if (roleToDelete == null)
        {
            throw new NotFoundExceptionWithStatusCode("Role not found");    
        }
        
        return await rolesRepository.DeleteRoleAsync(roleToDelete);
    }

    public async Task<UserRole> GetRoleInfoAsync(string role)
    {
        var roleInfo = await rolesRepository.GetRoleInfoAsync(role);
        if (roleInfo is null)
        {
            throw new NotFoundExceptionWithStatusCode("Role not found");
        }
        
        return roleInfo;
    }
}