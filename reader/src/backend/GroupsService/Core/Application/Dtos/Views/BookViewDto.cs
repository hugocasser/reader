using Domain.Models;

namespace Application.Dtos.Views;

public record BookViewDto(string BookName, string AuthorFirstName, string AuthorLastName);