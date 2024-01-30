using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models;

public class Category
{
    [BsonId, BsonElement("id")]
    public Guid Id { get; set; }
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;
}