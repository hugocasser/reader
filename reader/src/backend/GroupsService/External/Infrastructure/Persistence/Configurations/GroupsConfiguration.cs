using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class GroupsConfiguration : IEntityTypeConfiguration<Group>
{

    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasMany(group => group.Members)
            .WithMany(user => user.Groups);
        
        builder.HasMany(group => group.AllowedBooks)
            .WithMany(group => group.AllowedInGroups);

        builder.HasMany(group => group.GroupProgresses)
            .WithOne(progress => progress.Group);
        
    }
}