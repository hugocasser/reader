using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Persistence.Configurations;

public class RolesSeederConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasData(
            new UserRole("Admin")
            {
                Id = Guid.Parse("ab665c9c-5c85-4c90-972a-706cbaa896e3"), 
                NormalizedName = "ADMIN"
            },
            new UserRole("User")
            {
                Id = Guid.NewGuid(),
                NormalizedName = "USER"
            }
        );
    }
}