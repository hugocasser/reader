using System.ComponentModel.DataAnnotations;

namespace Presentation.Options;

public class ElasticOptions
{
    [Required]
    [Url]
    public string Uri { get; set; }
}