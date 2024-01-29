using Application.Common;
using Application.Dtos.Views.Books;
using Domain.Models;

namespace Application.Dtos.Views.Authors;

public record AuthorShortView(
    string LastName,
    string FirstName,
    DateTime BirthDay,
    DateTime DeathDay,
    string Biography)
{
    public static AuthorShortView MapFromModel(Author author, PageSettings pageSettings)
    {
        return new AuthorShortView(author.LastName, author.FirstName,
            author.BirthDate, author.DeathDate, author.Biography[..pageSettings.DescriptionMaxLength]);
    }
}