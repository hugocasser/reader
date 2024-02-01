using Application.Dtos.Requests;
using Domain.Models;

namespace Application.Dtos.Views.Books;

public record BookShortInfoView (string Name, string ShortDescription, string AuthorName, string CategoryName)
{
    public static BookShortInfoView MapFromModel(Book book, Author author, Category category, PageSetting pageSettings)
    {
        if (pageSettings.ShowDescription)
        {
            return new BookShortInfoView(book.Name,
                book.Description[..pageSettings.DescriptionMaxLength],
                author.FirstName + " " + author.LastName, category.Name);
        }
        return new BookShortInfoView(book.Name, string.Empty,
            author.FirstName + " " + author.LastName, category.Name);
    }
}