using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.Exceptions;

public class IdentityException(string? _errorMessage = null)
    : ExceptionWithStatusCode(StatusCodes.Status401Unauthorized, _errorMessage);