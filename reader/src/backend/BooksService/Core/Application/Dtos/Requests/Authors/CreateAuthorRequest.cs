using Application.Abstractions;
using Application.Validation;

namespace Application.Dtos.Requests.Authors;

public class CreateAuthorRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public DateTime DeathDate { get; set; }
    public string Biography { get; set; } = string.Empty;
}