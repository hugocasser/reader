using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Models;

public class UserRole : IdentityRole<Guid>
{
    public UserRole(string role) : base(role){}
    public UserRole():base(){}
}