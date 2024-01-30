using Domain.Models;
using Infrastructure.Persistence.Configurations;
using MongoDB.Driver;

namespace Infrastructure.Persistence;

public class MongoDbContext
{
    public IMongoCollection<Book> BooksCollection { get; set; }
    public IMongoCollection<Author> AuthorsCollection { get; set; }
    public IMongoCollection<Category> CategoriesCollection { get; set; }

    public MongoDbContext(MongoConfiguration configuration)
    {
        var client = new MongoClient(configuration.ConnectionUri);
        var database = client.GetDatabase(configuration.DatabaseName);
        BooksCollection = database.GetCollection<Book>(configuration.CollectionsNames.First());
        AuthorsCollection = database.GetCollection<Author>(configuration.CollectionsNames.ElementAt(1));
        CategoriesCollection = database.GetCollection<Category>(configuration.CollectionsNames.ElementAt(2));
    }
}