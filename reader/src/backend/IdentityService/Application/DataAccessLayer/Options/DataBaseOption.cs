using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Options;

public sealed class DataBaseOption
{
    [Required(AllowEmptyStrings = false)]
    public string ConnectionString { get; set; }
}