using Application.Dtos.Requests;
using Domain.Models;

namespace Application.Dtos.Views.Authors;

public record AuthorShortView(
    string LastName,
    string FirstName,
    DateTime BirthDay,
    DateTime DeathDay,
    string Biography)
{
    public static AuthorShortView MapFromModel(Domain.Models.Author author, PageSetting pageSettings)
    {
        if (pageSettings.ShowDescription)
        {
            return new AuthorShortView(author.LastName, author.FirstName,
                author.BirthDate, author.DeathDate, author.Biography[..pageSettings.DescriptionMaxLength]);
        }
        
        return new AuthorShortView(author.LastName, author.FirstName,
            author.BirthDate, author.DeathDate, string.Empty);
    }
}