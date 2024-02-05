using System.ComponentModel.DataAnnotations;

namespace Presentation.Options;

public sealed class TokenOptions
{
    [Required(AllowEmptyStrings = false)]
    public string Issuer { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Audience { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Key { get; set; }
}