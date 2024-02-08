using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.Exceptions;

public class NotFoundException(string _message) : ExceptionWithStatusCode(StatusCodes.Status404NotFound, _message);