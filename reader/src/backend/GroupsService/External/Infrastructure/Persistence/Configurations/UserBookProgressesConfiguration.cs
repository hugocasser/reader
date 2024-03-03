using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserBookProgressesConfiguration : IEntityTypeConfiguration<UserBookProgress>
{
    public void Configure(EntityTypeBuilder<UserBookProgress> builder)
    {
        builder.HasOne(progress => progress.User)
            .WithMany(user => user.UserBookProgresses);
        
        builder.HasOne(progress => progress.Group)
            .WithMany(group => group.GroupProgresses);
        
        builder.HasOne(progress => progress.Book)
            .WithMany(book => book.UserBookProgress);
    }
}