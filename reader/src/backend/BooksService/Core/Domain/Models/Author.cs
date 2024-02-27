using Domain.Abstractions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models;

public class Author : Entity
{
    [BsonElement("first_name")]
    public string FirstName { get; set; } = string.Empty;
    [BsonElement("last_name")]
    public string LastName { get; set; } = string.Empty;
    [BsonElement("birth_date"), BsonRepresentation(BsonType.DateTime)]
    public DateTime BirthDate { get; set; }
    [BsonElement("death_date"), BsonRepresentation(BsonType.DateTime)]
    public DateTime DeathDate { get; set; }
    [BsonElement("biography")]
    public string Biography { get; set; } = string.Empty;
}