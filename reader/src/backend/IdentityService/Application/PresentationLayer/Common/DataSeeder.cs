using DataAccessLayer.Models;
using DataAccessLayer.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace PresentationLayer.Common;

public static class DataSeeder
{
    
    public static User SeedAdmin(IOptions<AdminSeederOptions> options)
    {
        var admin = new User
        {
            Email = options.Value.Email,
            Id =  options.Value.Id,
            FirstName = "First Admin",
            LastName = "First Admin",
            UserName = "FirstAdmin",
            NormalizedEmail = "test_identity_Admin_reader2024@gmail.com".ToUpper(),
            NormalizedUserName = "FIRSTADMIN",
            SecurityStamp = Guid.NewGuid().ToString("D"),
        };
        var passwordHasher = new PasswordHasher<User>();
        admin.PasswordHash = passwordHasher.HashPassword(admin, options.Value.Password);
        
        return admin;
    }

    public static IdentityUserRole<Guid> SeedAdminWithRoles(IOptions<AdminSeederOptions> options)
    {
        return new IdentityUserRole<Guid>()
        {
            RoleId = options.Value.Id,
            UserId = Guid.Parse("343a57ec-6dde-47d1-95ee-34cd2362da88")
        };
    }

    public static IEnumerable<UserRole> SeedUserRoles(IOptions<AdminSeederOptions> options)
    {

        var adminRoles = new List<UserRole>(2)
        {
            new UserRole("Admin")
            {
                Id = options.Value.Id,
                NormalizedName = "ADMIN"
            },
            new UserRole("User")
            {
                Id = Guid.NewGuid(),
                NormalizedName = "USER"
            }
        };
        
        return adminRoles;
    }
}