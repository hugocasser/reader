using Application.Abstractions.Repositories;
using Domain.Models;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository(WriteDbContext _writeDbContext, ReadDbContext _readDbContext)
    : BaseRepository<User>(_writeDbContext, _readDbContext), IUsersRepository;