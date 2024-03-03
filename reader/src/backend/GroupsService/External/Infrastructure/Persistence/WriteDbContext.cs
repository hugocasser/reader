using Domain.Models;
using Infrastructure.Interceptor;
using Infrastructure.OutboxMessages;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class WriteDbContext(DbContextOptions<WriteDbContext> options) : DbContext(options)
{
    private static readonly ConvertDomainEventsToOutboxMessagesInterceptor _messagesInterceptor = new();
    
    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<UserBookProgress> UserBookProgresses { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WriteDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.AddInterceptors(_messagesInterceptor);
    }
}