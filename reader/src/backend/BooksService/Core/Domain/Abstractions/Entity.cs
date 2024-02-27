using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Abstractions;

public abstract class Entity
{
   [BsonId, BsonElement("id")]
   public Guid Id { get; set; }
}