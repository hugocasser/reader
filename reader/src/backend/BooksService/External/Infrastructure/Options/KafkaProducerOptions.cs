using System.ComponentModel.DataAnnotations;
using Confluent.Kafka;

namespace Infrastructure.Options;

public class KafkaProducerOptions
{
    [Required(AllowEmptyStrings = false)]
    public string BootstrapServers { get; set; }
}