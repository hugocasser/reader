using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Common;
using Application.Dtos.Requests;
using Application.Dtos.Requests.Authors;
using Application.Dtos.Views.Authors;
using Application.Exceptions;
using Domain.Models;

namespace Application.Services;

public class AuthorsService(IAuthorRepository authorRepository): IAuthorsService
{
    public async Task CreateAuthorAsync(CreateAuthorRequest request)
    {
        await authorRepository.AddAuthorAsync(new Author
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            BirthDate = request.BirthDate,
            DeathDate = request.DeathDate,
            Biography = request.Biography
        });
    }

    public async Task<IEnumerable<AuthorShortView>> GetAllAuthorsAsync(PageSettings pageSettings)
    {
        var authors = await authorRepository.GetAuthorsAsync();
        var orderedAuthors = authors.OrderBy(author => author.LastName)
            .ThenBy(author => author.FirstName).ThenBy(author => author.BirthDate)
            .Skip(pageSettings.PageNumber-1*pageSettings.PageSize).Take(pageSettings.PageSize);
        
        return orderedAuthors.Select(author =>
            AuthorShortView.MapFromModel(author, pageSettings)).ToList();
    }

    public async Task<Author> GetAuthorByIdAsync(Guid id)
    {
        var author = await authorRepository.GetAuthorByIdAsync(id);
        
        if (author is null)
        {
            throw new NotFoundExceptionWithStatusCode("Author with this id not found");
        }
        
        return author;
    }

    public async Task DeleteByIdAuthorAsync(Guid id)
    {
        if (!await authorRepository.AuthorExistsAsync(id))
        {
            throw new NotFoundExceptionWithStatusCode("Author with this id not found");
        }
        
        await authorRepository.DeleteByIdAuthorAsync(id);
    }

    public async Task UpdateAuthorAsync(UpdateAuthorsRequest request)
    {
        var author = await GetAuthorByIdAsync(request.Id);

        if (author is null)
        {
            throw new NotFoundExceptionWithStatusCode("Author with this id not found");
        }
        
        author.FirstName = request.FirstName;
        author.LastName = request.LastName;
        author.BirthDate = request.BirthDate;
        author.DeathDate = request.DeathDate;
        author.Biography = request.Biography;
        
        await authorRepository.UpdateAuthorAsync(author);
    }
}