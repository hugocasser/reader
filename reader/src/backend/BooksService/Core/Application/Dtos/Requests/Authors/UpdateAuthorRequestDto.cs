namespace Application.Dtos.Requests.Authors;

public record UpdateAuthorRequestDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime DeathDate { get; set; }
    public string Biography { get; set; }
}