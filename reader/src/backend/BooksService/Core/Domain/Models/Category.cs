using Domain.Abstractions;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models;

public class Category : Entity
{
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;
}