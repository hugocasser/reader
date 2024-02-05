using Domain.Models;

namespace Application.Dtos.Views.Authors;

public record AuthorView(string FirstName, string LastName, DateTime BirthDay, DateTime DeathDay, string Biography)
{
    public static AuthorView MapFromModel(Author authorView)
    {
        return new AuthorView(authorView.FirstName, authorView.LastName, authorView.BirthDate, authorView.DeathDate, authorView.Biography);
    }
}