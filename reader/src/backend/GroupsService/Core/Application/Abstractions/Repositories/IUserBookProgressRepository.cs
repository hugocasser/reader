using Application.Dtos.Views;
using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IUserBookProgressRepository: IBaseRepository<UserBookProgress, ProgressViewDto>;