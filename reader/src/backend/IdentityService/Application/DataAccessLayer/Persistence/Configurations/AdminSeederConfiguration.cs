using DataAccessLayer.Exeptions;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Persistence.Configurations;

public class AdminSeederConfiguration : IEntityTypeConfiguration<User>
{
    private readonly IConfiguration _configuration;

    public AdminSeederConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void Configure(EntityTypeBuilder<User> builder)
    {
        Guid id;
        string email, password;
        
            id = Guid.Parse(_configuration["Admin:Id"] ?? throw new UserSecretsInvalidException("setup-admin-id-secret"));
            email = _configuration["Admin:Email"] ?? throw new UserSecretsInvalidException("setup-admin-email-secret");
            password = _configuration["Admin:Password"] ?? throw new UserSecretsInvalidException("setup-admin-password-secret");

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