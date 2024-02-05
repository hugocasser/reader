using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Dtos.Requests;
using Application.Dtos.Requests.Authors;
using Application.Dtos.Views.Authors;
using Application.Exceptions;
using Domain.Models;

namespace Application.Services;

public class AuthorsService(IAuthorsRepository authorsRepository): IAuthorsService
{
    public async Task CreateAuthorAsync(CreateAuthorRequest request, CancellationToken cancellationToken)
    {
        await authorsRepository.AddAuthorAsync(new Author
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            BirthDate = request.BirthDate,
            DeathDate = request.DeathDate,
            Biography = request.Biography
        }, cancellationToken);
    }

    public async Task<IEnumerable<AuthorShortView>> GetAllAuthorsAsync(PageSetting pageSettings,
        CancellationToken cancellationToken)
    {
        var authors = await authorsRepository
            .GetAuthorsAsync(pageSettings.PageSize,
                pageSettings.PageNumber*(pageSettings.PageSize-1), cancellationToken);
        
        return authors.Select(author =>
            AuthorShortView.MapFromModel(author, pageSettings)).ToList();
    }

    public async Task<AuthorView> GetAuthorByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var author = await authorsRepository.GetAuthorByIdAsync(id, cancellationToken);
        
        if (author is null)
        {
            throw new NotFoundException("Author with this id not found");
        }
        
        return AuthorView.MapFromModel(author);
    }

    public async Task DeleteByIdAuthorAsync(Guid id, CancellationToken cancellationToken)
    {
        if (!await authorsRepository.AuthorExistsAsync(id, cancellationToken))
        {
            throw new NotFoundException("Author with this id not found");
        }
        
        await authorsRepository.DeleteByIdAuthorAsync(id, cancellationToken);
    }

    public async Task UpdateAuthorAsync(UpdateAuthorRequest request, CancellationToken cancellationToken)
    {
        var author = await authorsRepository.GetAuthorByIdAsync(request.Id, cancellationToken);

        if (author is null)
        {
            throw new NotFoundException("Author with this id not found");
        }
        
        author.FirstName = request.FirstName;
        author.LastName = request.LastName;
        author.BirthDate = request.BirthDate;
        author.DeathDate = request.DeathDate;
        author.Biography = request.Biography;
        
        await authorsRepository.UpdateAuthorAsync(author, cancellationToken);
    }
}