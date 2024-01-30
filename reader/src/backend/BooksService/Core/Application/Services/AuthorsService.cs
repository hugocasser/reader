using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Common;
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

    public async Task<IEnumerable<AuthorShortView>> GetAllAuthorsAsync(PageSettings pageSettings,
        CancellationToken cancellationToken)
    {
        var authors = await authorsRepository
            .GetAuthorsAsync(pageSettings.PageSize,
                pageSettings.PageNumber*(pageSettings.PageSize-1), cancellationToken);
        var orderedAuthors = authors.OrderBy(author => author.LastName)
            .ThenBy(author => author.FirstName).ThenBy(author => author.BirthDate)
            .Skip(pageSettings.PageNumber-1*pageSettings.PageSize).Take(pageSettings.PageSize);
        
        return orderedAuthors.Select(author =>
            AuthorShortView.MapFromModel(author, pageSettings)).ToList();
    }

    public async Task<Author> GetAuthorByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var author = await authorsRepository.GetAuthorByIdAsync(id, cancellationToken);
        
        if (author is null)
        {
            throw new NotFoundExceptionWithStatusCode("Author with this id not found");
        }
        
        return author;
    }

    public async Task DeleteByIdAuthorAsync(Guid id, CancellationToken cancellationToken)
    {
        if (!await authorsRepository.AuthorExistsAsync(id, cancellationToken))
        {
            throw new NotFoundExceptionWithStatusCode("Author with this id not found");
        }
        
        await authorsRepository.DeleteByIdAuthorAsync(id, cancellationToken);
    }

    public async Task UpdateAuthorAsync(UpdateAuthorRequest request, CancellationToken cancellationToken)
    {
        var author = await GetAuthorByIdAsync(request.Id, cancellationToken);

        if (author is null)
        {
            throw new NotFoundExceptionWithStatusCode("Author with this id not found");
        }
        
        author.FirstName = request.FirstName;
        author.LastName = request.LastName;
        author.BirthDate = request.BirthDate;
        author.DeathDate = request.DeathDate;
        author.Biography = request.Biography;
        
        await authorsRepository.UpdateAuthorAsync(author, cancellationToken);
    }
}