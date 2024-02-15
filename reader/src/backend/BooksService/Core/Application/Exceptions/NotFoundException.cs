using Microsoft.AspNetCore.Http;

namespace Application.Exceptions;

public class NotFoundException(string message)
    : ExceptionWithStatusCode(StatusCodes.Status404NotFound, message);