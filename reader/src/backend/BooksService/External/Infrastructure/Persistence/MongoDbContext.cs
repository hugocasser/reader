using Domain.Models;
using Infrastructure.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Persistence;

public class MongoDbContext 
{
    public IMongoCollection<Book> BooksCollection { get; set; }
    public IMongoCollection<Author> AuthorsCollection { get; set; }
    public IMongoCollection<Category> CategoriesCollection { get; set; }

    public MongoDbContext(IOptions<MongoOptions> options, IMongoClient client)
    {
        var database = client.GetDatabase(options.Value.DatabaseName);
        BooksCollection = database.GetCollection<Book>(options.Value.CollectionsNames.First());
        AuthorsCollection = database.GetCollection<Author>(options.Value.CollectionsNames.ElementAt(1));
        CategoriesCollection = database.GetCollection<Category>(options.Value.CollectionsNames.ElementAt(2));
    }
}