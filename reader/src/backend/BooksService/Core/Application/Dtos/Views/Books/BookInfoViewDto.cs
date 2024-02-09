namespace Application.Dtos.Views.Books;

public record BookInfoViewDto()
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid AuthorId { get; set; }
    public Guid CategoryId { get; set; }
    //public string Description { get; set; }
};