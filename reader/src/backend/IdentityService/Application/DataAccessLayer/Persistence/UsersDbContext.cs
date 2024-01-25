using System.Reflection;
using DataAccessLayer.Models;
using DataAccessLayer.Persistence.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RolesSeederConfiguration = DataAccessLayer.Persistence.Configurations.RolesSeederConfiguration;

namespace DataAccessLayer.Persistence;

public class UsersDbContext : IdentityDbContext<User, UserRole, Guid>
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    private readonly IConfiguration _configuration;

    public UsersDbContext(
        DbContextOptions options,
        IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        modelBuilder.ApplyConfiguration(new AdminSeederConfiguration(_configuration));
        modelBuilder.ApplyConfiguration(new RefreshTokensConfiguration());
    }
}