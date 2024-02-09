namespace Application.Dtos.Views.Authors;

public record AuthorViewDto()
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDay { get; set; }
    public DateTime DeathDay { get; set; }
    public string Biography { get; set; }
}