using BusinessLogicLayer.Validation;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;

public class RegisterUserRequestDto : BaseValidationModel<RegisterUserRequestDto>
{
    public string Email { get; set; } 
    public string Password { get; set; } 
    public string? FirstName { get; set; } 
    public string? LastName { get; set; } 
    public string Username { get; init; } 

    public User ToUser()
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
            UserName = Username
        };
    }
}