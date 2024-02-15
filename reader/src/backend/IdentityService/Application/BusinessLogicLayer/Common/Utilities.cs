using System.Text;
using BusinessLogicLayer.Exceptions;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Common;

public static class Utilities
{
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    
    public static void AggregateIdentityErrorsAndThrow(IdentityResult result)
    {
        if (result.Succeeded) return;
        
        var errors = result.Errors.Aggregate(string.Empty,
            (current, error) => current + (error.Description + "\n"));
        throw new IdentityException(errors);
    }

    public static string GenerateRandomString(int length)
    {
        var random = new Random();
        var result = new StringBuilder(length);
        
        for (var i = 0; i < length; i++)
        {
            result.Append(Chars[random.Next(Chars.Length)]);
        }
        
        return result.ToString();
    }

    public static IQueryable<User> GetUsers(this UserManager<User> userManager, int page, int pageSize)
    {
        return userManager.Users.AsNoTracking().OrderBy(user => user.Email)
            .ThenBy(user => user.UserName)
            .Skip((page-1)*pageSize).Take(pageSize);
    }
}