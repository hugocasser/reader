using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.Exceptions;

public class IdentityException(string? errorMessage = null)
    : ExceptionWithStatusCode(StatusCodes.Status401Unauthorized, errorMessage);