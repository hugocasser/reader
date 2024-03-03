using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(user => user.AdminGroup)
            .WithOne(group => group.Admin)
            .HasForeignKey<Group>(group => group.AdminId)
            .IsRequired();
    }
}