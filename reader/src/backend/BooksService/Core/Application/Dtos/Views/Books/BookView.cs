using Domain.Models;

namespace Application.Dtos.Views.Books;

public record BookView(string Name, string Text, string AuthorName, string CategoryName)
{
    public static BookView MapFromModel(Book book, Author author, Category category)
    {
        return new BookView(book.Name, book.Text, author.FirstName + " " + author.LastName, category.Name);
    }
};