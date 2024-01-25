using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.Exceptions;

public class NotFoundExceptionWithStatusCode : ExceptionWithStatusCode
{
    public NotFoundExceptionWithStatusCode(string message) : base(StatusCodes.Status404NotFound, message)
    {
    }
}