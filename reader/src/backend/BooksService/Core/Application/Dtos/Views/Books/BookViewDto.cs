namespace Application.Dtos.Views.Books;

public record BookViewDto()
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Text { get; set; }
    public Guid AuthorId { get; set; }
    public Guid CategoryId { get; set; }
};