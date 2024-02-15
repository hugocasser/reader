using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.Exceptions;

public class IdentityExceptionWithStatusCode : ExceptionWithStatusCode
{
    public IdentityExceptionWithStatusCode(string? errorMessage = null) : base(StatusCodes.Status401Unauthorized, errorMessage)
    {
    }
}