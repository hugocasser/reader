using Application.Abstractions;
using Application.Validation;

namespace Application.Dtos.Requests.Books;

public class UpdateBookInfoRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid AuthorId { get; set; }
    public Guid CategoryId { get; set; }
}