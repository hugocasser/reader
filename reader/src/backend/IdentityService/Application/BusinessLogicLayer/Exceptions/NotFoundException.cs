using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.Exceptions;

public class NotFoundException(string message) : ExceptionWithStatusCode(StatusCodes.Status404NotFound, message);