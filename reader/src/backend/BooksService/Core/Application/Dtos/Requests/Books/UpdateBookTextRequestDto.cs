namespace Application.Dtos.Requests.Books;

public class UpdateBookTextRequestDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
}