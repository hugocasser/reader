using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Options;

public class TokenGenerationOptions 
{
    [Required(AllowEmptyStrings = false)]
    public string Issuer { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Audience { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Key { get; set; }
}