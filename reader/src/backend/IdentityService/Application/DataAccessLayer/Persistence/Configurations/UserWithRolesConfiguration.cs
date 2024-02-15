using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Persistence.Configurations;

public class UserWithRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
    {
        builder.HasData(
            new IdentityUserRole<Guid>()
            {
                RoleId = Guid.Parse("ab665c9c-5c85-4c90-972a-706cbaa896e3"), 
                UserId = Guid.Parse("343a57ec-6dde-47d1-95ee-34cd2362da88")
            }
        );
    }
}