using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class BooksConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasMany(book => book.UserBookProgress)
            .WithOne(progress => progress.Book);

        builder.HasMany(book => book.AllowedInGroups)
            .WithMany(group => group.AllowedBooks);
    }
}