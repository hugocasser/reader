using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.Exceptions;

public class IdentityExceptionWithStatusCode(string? errorMessage = null)
    : ExceptionWithStatusCode(StatusCodes.Status401Unauthorized, errorMessage);