using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.Exceptions;

public class BadRequestException(string message) : ExceptionWithStatusCode(StatusCodes.Status400BadRequest, message);