using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(user => user.UserBookProgresses)
            .WithOne(progress => progress.User);
        
        builder.HasMany(user => user.Groups)
            .WithMany(group => group.Members);
    }
}