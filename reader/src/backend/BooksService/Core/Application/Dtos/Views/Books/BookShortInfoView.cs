using Application.Common;
using Domain.Models;

namespace Application.Dtos.Views.Books;

public record BookShortInfoView (string Name, string ShortDescription, string AuthorName, string CategoryName)
{
    public static BookShortInfoView MapFromModel(Book book, Author author, Category category, PageSettings pageSettings)
    {
        if (pageSettings.showDescription)
        {
            return new BookShortInfoView(book.Name,
                book.Description[..pageSettings.DescriptionMaxLength],
                author.FirstName + " " + author.LastName, category.Name);
        }
        return new BookShortInfoView(book.Name, string.Empty,
            author.FirstName + " " + author.LastName, category.Name);
    }
}