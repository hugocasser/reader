using Application.Dtos.Views;
using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IUsersRepository : IBaseRepository<User, UserViewDto>;