using Application.Abstractions;
using Application.Validation;

namespace Application.Dtos.Requests.Books;

public class UpdateBookTextRequest
{
    public Guid Id { get; set; }
    public string Text { get; set; }
}