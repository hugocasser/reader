using DataAccessLayer.Models;
using DataAccessLayer.Persistence.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Persistence;

public class UsersDbContext(
    DbContextOptions options,
    IConfiguration configuration) : IdentityDbContext<User, UserRole, Guid>(options)
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new AdminSeederConfiguration(configuration));
        modelBuilder.ApplyConfiguration(new RolesSeederConfiguration());
        modelBuilder.ApplyConfiguration(new UserWithRolesConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokensConfiguration());
    }
}