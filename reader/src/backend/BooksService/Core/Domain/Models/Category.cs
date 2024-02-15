using Domain.Abstractions;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models;

public class Category : IEntity
{
    [BsonId, BsonElement("id")]
    public Guid Id { get; set; }
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;
}