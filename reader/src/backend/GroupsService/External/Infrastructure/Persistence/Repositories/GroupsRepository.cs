using Application.Abstractions.Repositories;
using Application.Dtos.Requests;
using Application.Dtos.Views;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class GroupsRepository(WriteDbContext _writeDbContext, ReadDbContext _readDbContext)
    : BaseRepository<Group, GroupViewDto>(_writeDbContext, _readDbContext), IGroupsRepository
{
    public Task CreateGroupAsync(Group group, CancellationToken cancellationToken)
    {
        _writeDbContext.Entry(group.Admin).State = EntityState.Modified;
        _writeDbContext.Entry(group).State = EntityState.Added;
        
        return CreateAsync(group, cancellationToken); 
    }

    public Task CreateGroupAsyncInReadDbContextAsync(Group group, CancellationToken cancellationToken)
    {
        _readDbContext.Entry(group.Admin).State = EntityState.Modified;
        _readDbContext.Entry(group).State = EntityState.Added;
        
        return CreateAsyncInReadDbContextAsync(group, cancellationToken);
    }
}