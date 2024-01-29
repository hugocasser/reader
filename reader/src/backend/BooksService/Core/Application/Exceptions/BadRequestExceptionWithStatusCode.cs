using Microsoft.AspNetCore.Http;

namespace Application.Exceptions;

public class BadRequestExceptionWithStatusCode(string message)
    : ExceptionWithStatusCode(StatusCodes.Status400BadRequest, message);