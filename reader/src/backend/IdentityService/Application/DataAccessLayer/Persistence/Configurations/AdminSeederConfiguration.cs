using DataAccessLayer.Exeptions;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Persistence.Configurations;

public class AdminSeederConfiguration(IConfiguration configuration) : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        Guid id;
        string email, password;
        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TEST_ConnectionString")))
        {
            id = Guid.Parse("343a57ec-6dde-47d1-95ee-34cd2362da88");
            email = "test_identity_Admin_reader2024@gmail.com";
            password = "test_identity_Admin_reader2024@gmail.com";
            
            return;
        }
        else
        {
            id = Guid.Parse(configuration["Admin:Id"] ?? throw new UserSecretsInvalidException("setup-admin-id-secret"));
            email = configuration["Admin:Email"] ?? throw new UserSecretsInvalidException("setup-admin-email-secret");
            password = configuration["Admin:Password"] ??
                       throw new UserSecretsInvalidException("setup-admin-password-secret");
        }

        var user = new User
        {
            Id = id,
            UserName = "FirstAdmin",
            Email = email,
            NormalizedEmail = email.ToUpper(),
            NormalizedUserName = "FIRST ADMIN",
            EmailConfirmed = true,
            FirstName = "First Admin",
            LastName = "First Admin",
            SecurityStamp = Guid.NewGuid().ToString("D"),
        };

        var passwordHasher = new PasswordHasher<User>();
        user.PasswordHash = passwordHasher.HashPassword(user, password);
        builder.HasData(user);
    }
}