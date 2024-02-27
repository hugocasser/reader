using Application.Abstractions.Repositories;
using Application.Dtos.Views;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserBookProgressRepository
    (WriteDbContext _writeDbContext, ReadDbContext _readDbContext)
    : BaseRepository<UserBookProgress, ProgressViewDto>(_writeDbContext, _readDbContext), IUserBookProgressRepository;