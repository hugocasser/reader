using Application.Abstractions;

namespace Application.Dtos.Requests.Books;

public class CreateBookRequest : IRequest
{
    public string Description { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public Guid AuthorId { get; set; }
    public Guid CategoryId { get; set; }
}