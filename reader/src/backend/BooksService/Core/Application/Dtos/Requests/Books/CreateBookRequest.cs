namespace Application.Dtos.Requests.Books;

public class CreateBookRequest
{
    public string Description { get; set; }
    public string Name { get; set; }
    public string Text { get; set; }
    public Guid AuthorId { get; set; }
    public Guid CategoryId { get; set; }
}
