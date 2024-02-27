using Application.Abstractions.Repositories;
using Application.Dtos.Requests;
using Application.Dtos.Views;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class GroupsRepository(WriteDbContext _writeDbContext, ReadDbContext _readDbContext)
    : BaseRepository<Group, GroupViewDto>(_writeDbContext, _readDbContext), IGroupsRepository;