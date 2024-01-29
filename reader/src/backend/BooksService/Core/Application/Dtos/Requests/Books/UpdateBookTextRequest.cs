using Application.Abstractions;

namespace Application.Dtos.Requests.Books;

public class UpdateBookTextRequest : IRequest
{
    public Guid Id { get; set; }
    public string Text { get; set; }
}