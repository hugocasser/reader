namespace Application.Dtos.Views.Books;

public record BookShortInfoViewDto () 
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid AuthorId { get; set; }
    public Guid CategoryId { get; set; }
}