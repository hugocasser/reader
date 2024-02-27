using System.ComponentModel.DataAnnotations;
using Confluent.Kafka;

namespace Infrastructure.Options;

public class KafkaConsumerOptions
{
    [Required(AllowEmptyStrings = false)]
    public string BootstrapServers { get; set; }
}