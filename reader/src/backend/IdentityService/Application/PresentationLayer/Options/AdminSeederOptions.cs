using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Options;

public sealed class AdminSeederOptions
{
    [Required(AllowEmptyStrings = false)]
    public Guid Id { get; set; }
    [Required(AllowEmptyStrings = false)]
    [EmailAddress]
    public string Email { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Password { get; set; }
}