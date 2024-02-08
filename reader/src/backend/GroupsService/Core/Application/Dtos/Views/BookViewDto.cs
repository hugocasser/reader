using Domain.Models;

namespace Application.Dtos.Views;

public record BookViewDto(string BookName, string AuthorFirstName, string AuthorLastName)
{
    public static BookViewDto MapFromModel(Book book)
    {
        return new BookViewDto(book.BookName, book.AuthorFirstName, book.AuthorLastName);
    }
};