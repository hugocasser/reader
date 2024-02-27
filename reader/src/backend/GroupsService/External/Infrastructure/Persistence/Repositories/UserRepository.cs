using Application.Abstractions.Repositories;
using Application.Dtos.Views;
using Domain.Models;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository(WriteDbContext _writeDbContext, ReadDbContext _readDbContext)
    : BaseRepository<User, UserViewDto>(_writeDbContext, _readDbContext), IUsersRepository;