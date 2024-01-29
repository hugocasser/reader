using Microsoft.AspNetCore.Http;

namespace Application.Exceptions;

public class NotFoundExceptionWithStatusCode(string message)
    : ExceptionWithStatusCode(StatusCodes.Status404NotFound, message);