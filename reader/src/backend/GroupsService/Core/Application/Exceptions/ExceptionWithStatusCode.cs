using System.Net;

namespace Application.Exceptions;

public class ExceptionWithStatusCode(HttpStatusCode statusCode, string message) : Exception(message)
{
    public HttpStatusCode StatusCode { get; set; } = statusCode;
}