using Application.Abstractions;
using Application.Validation;

namespace Application.Dtos.Requests.Books;

public class CreateBookRequest
{
    public string Description { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public Guid AuthorId { get; set; }
    public Guid CategoryId { get; set; }
}
