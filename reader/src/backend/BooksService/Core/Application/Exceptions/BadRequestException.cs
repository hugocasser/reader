using Microsoft.AspNetCore.Http;

namespace Application.Exceptions;

public class BadRequestException(string message)
    : ExceptionWithStatusCode(StatusCodes.Status400BadRequest, message);