using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Options;

public class RedisOptions
{
    [Required(AllowEmptyStrings = false)]
    public string ConnectionString { get; set; }
}