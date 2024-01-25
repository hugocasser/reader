using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.Exceptions;

public class BadRequestExceptionWithStatusCode : ExceptionWithStatusCode
{
    public BadRequestExceptionWithStatusCode(string message) : base(StatusCodes.Status400BadRequest, message)
    {
        
    }
}