using System.ComponentModel.DataAnnotations;

namespace Presentation.Options;

public sealed class MongoOptions
{
    [Required(AllowEmptyStrings = false)]
    [Url]
    public string ConnectionUri { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string DatabaseName { get; set; }
    [Required(AllowEmptyStrings = false)]
    public IEnumerable<string> CollectionsNames { get; set; }
}