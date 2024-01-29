using Domain.Models;

namespace Application.Dtos.Views.Authors;

public record AuthorView(string FirstName, string LastName, DateTime BirthDay, DateTime DeathDay, string Biography)
{
    public static AuthorView MapFromModel(Author author)
    {
        return new AuthorView(author.FirstName, author.LastName, author.BirthDate, author.DeathDate, author.Biography);
    }
}