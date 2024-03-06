using System.ComponentModel.DataAnnotations;

namespace Application.Options;

public class KafkaProducerOptions
{
    [Required(AllowEmptyStrings = false)]
    public string BootstrapServers { get; set; }
}