namespace Presentation.Exceptions;

public class NotValidClaimsException(string message) : Exception(message)
{
    public int Code = 400;
}