using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.Exceptions;

public class BadRequestException(string _message) : ExceptionWithStatusCode(StatusCodes.Status400BadRequest, _message);