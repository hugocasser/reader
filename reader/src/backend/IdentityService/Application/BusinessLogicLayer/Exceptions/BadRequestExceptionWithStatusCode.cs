using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.Exceptions;

public class BadRequestExceptionWithStatusCode(string message)
    : ExceptionWithStatusCode(StatusCodes.Status400BadRequest, message);