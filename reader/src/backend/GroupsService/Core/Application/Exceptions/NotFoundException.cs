using System.Net;

namespace Application.Exceptions;

public class NotFoundException(string message):
    ExceptionWithStatusCode(HttpStatusCode.NotFound, message);