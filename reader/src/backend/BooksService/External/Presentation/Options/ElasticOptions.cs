using System.ComponentModel.DataAnnotations;

namespace Presentation.Options;

public sealed class ElasticOptions
{
    [Required]
    [Url]
    public string Uri { get; set; }
}