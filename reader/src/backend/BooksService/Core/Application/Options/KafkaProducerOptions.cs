using System.ComponentModel.DataAnnotations;
<<<<<<< HEAD:reader/src/backend/BooksService/External/Infrastructure/Options/KafkaProducerOptions.cs
using Confluent.Kafka;

namespace Infrastructure.Options;
=======

namespace Application.Options;
>>>>>>> feature-kafka-message-broker:reader/src/backend/BooksService/Core/Application/Options/KafkaProducerOptions.cs

public class KafkaProducerOptions
{
    [Required(AllowEmptyStrings = false)]
    public string BootstrapServers { get; set; }
}