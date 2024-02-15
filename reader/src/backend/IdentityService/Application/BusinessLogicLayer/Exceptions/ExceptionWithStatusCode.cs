namespace BusinessLogicLayer.Exceptions;

public class ExceptionWithStatusCode(int _statusCode, string? _errorMessage = null) : Exception(_errorMessage)
{
    public int StatusCode { get; } = _statusCode;
    public string? ErrorMessage { get; } = _errorMessage;
}