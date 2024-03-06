using Domain.Abstractions;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models;

public class Book : Entity
{
    [BsonElement ("description")]
    public string Description { get; set; } = string.Empty;
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;
    [BsonElement("text")]
    public string Text { get; set; } = string.Empty;
    [BsonElement("category_id")]
    public Guid CategoryId { get; set; }
    [BsonElement("author_id")]
    public Guid AuthorId { get; set; }
    [BsonIgnore]
    public string AuthorFirstName { get; set; } = string.Empty;
    [BsonIgnore]
    public string AuthorLastName { get; set; } = string.Empty;
}