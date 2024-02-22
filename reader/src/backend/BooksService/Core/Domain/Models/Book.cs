using Domain.Abstractions;
using Domain.Abstractions.Events;
using Domain.Events;
using Domain.Messages;
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

    public GenericDomainEvent<Book> CreateBook(string description,
        string name,
        string text,
        Guid authorId,
        Guid categoryId,
        string authorFirstName,
        string authorLastName)
    {
        Id = new Guid();
        Description = description;
        Name = name;
        Text = text;
        AuthorId = authorId;
        CategoryId = categoryId;
        AuthorFirstName = authorFirstName;
        AuthorLastName = authorLastName;
        
        return new GenericDomainEvent<Book>(this, EventType.Created);
    }

    public GenericDomainEvent<Book> UpdateBook(string? authorFirstName = null, string? authorLastName = null,
        string? name = null,
        Guid? categoryId = null,
        string? description = null,
        string? text = null,
        Guid? authorId = null)
    {
        if (categoryId is not null)
        {
            CategoryId = categoryId.Value;
        }

        if (description is not null)
        {
            Description = description;
        }

        if (text is not null)
        {
            Text = text;
        }

        if (authorId is not null)
        {
            AuthorId = authorId.Value;
        }

        if (name is not null)
        {
            Name = name;
        }

        return new GenericDomainEvent<Book>(this, EventType.Updated);
    }

    public GenericDomainEvent<Book> DeleteBook()
    {
        return new GenericDomainEvent<Book>(this, EventType.Deleted);
    }
}