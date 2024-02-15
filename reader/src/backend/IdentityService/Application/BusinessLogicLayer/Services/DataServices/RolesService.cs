using BusinessLogicLayer.Abstractions.Services;
using BusinessLogicLayer.Abstractions.Services.DataServices;
using BusinessLogicLayer.Exceptions;
using DataAccessLayer.Abstractions.Repositories;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.Services.DataServices;

public class RolesService : IRolesService
{
    private readonly IUserRolesRepository _rolesRepository;

    public RolesService(IUserRolesRepository rolesRepository)
    {
        _rolesRepository = rolesRepository;
    }
    
    public async Task<IdentityResult> CreateRoleAsync(string? role)
    {
        if (string.IsNullOrEmpty(role))
        {
            throw new BadRequestExceptionWithStatusCode("Role cannot be null or empty");
        }
        
        return await _rolesRepository.CreateRoleAsync(role);
    }
    

    public async Task<IdentityResult> DeleteRoleAsync(string role)
    {
        if (string.IsNullOrEmpty(role))
        {
            throw new BadRequestExceptionWithStatusCode("Role cannot be null or empty");
        }
        
        var roleToDelete = await _rolesRepository.GetRoleInfoAsync(role);

        if (roleToDelete == null)
        {
            throw new NotFoundExceptionWithStatusCode("Role not found");    
        }
        
        return await _rolesRepository.DeleteRoleAsync(roleToDelete);
    }

    public async Task<bool> RoleExistsAsync(string? role)
    {
        if (string.IsNullOrEmpty(role))
        {
            throw new BadRequestExceptionWithStatusCode("Role cannot be null or empty");
        }
        
        return await _rolesRepository.RoleExistsAsync(role);
    }

    public async Task<UserRole> GetRoleInfoAsync(string role)
    {
        var roleInfo = await _rolesRepository.GetRoleInfoAsync(role);
        if (roleInfo is null)
        {
            throw new NotFoundExceptionWithStatusCode("Role not found");
        }
        return roleInfo;
    }
}