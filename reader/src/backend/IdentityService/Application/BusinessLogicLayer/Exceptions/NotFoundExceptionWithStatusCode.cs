using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.Exceptions;

public class NotFoundExceptionWithStatusCode(string message)
    : ExceptionWithStatusCode(StatusCodes.Status404NotFound, message);