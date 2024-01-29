using System.Text;
using BusinessLogicLayer.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.Common;

public static class Utilities
{
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    
    public static void AggregateIdentityErrorsAndThrow(IdentityResult result)
    {
        if (result.Succeeded) return;
        var errors = result.Errors.Aggregate(string.Empty, (current, error) => current
                                                                               + (error.Description + "\n"));
        throw new IdentityExceptionWithStatusCode(errors);
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
}