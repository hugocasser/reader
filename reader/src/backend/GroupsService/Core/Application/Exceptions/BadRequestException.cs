using System.Net;

namespace Application.Exceptions;

public class BadRequestException(string message) : ExceptionWithStatusCode(HttpStatusCode.BadRequest, message)
{
    
}