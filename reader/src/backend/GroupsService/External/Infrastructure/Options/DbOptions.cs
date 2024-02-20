using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Options;

public sealed class DbOptions
{
    [Required(AllowEmptyStrings = false)]
    public string ReadConnectionString { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string WriteConnectionString { get; set; }
}