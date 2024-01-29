using Domain.Models;

namespace Application.Dtos.Views.Books;

public record BookInfoView(string Name, string AuthorName, string CategoryName, string Description)
{
    public static BookInfoView MapFromModel(Book book, Author author, Category category)
    {
        return new BookInfoView(book.Name,
            author.FirstName + " " + author.LastName, category.Name, book.Description);
    }
};