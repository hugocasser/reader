using Microsoft.AspNetCore.Http;

namespace Application.Exceptions;

public class ExceptionWithStatusCode(int statusCode, string? errorMessage = null) : Exception(errorMessage)
{
    public int StatusCode { get; } = statusCode;
    public string? ErrorMessage { get; } = errorMessage;
}