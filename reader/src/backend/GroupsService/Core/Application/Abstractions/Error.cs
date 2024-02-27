namespace Application.Abstractions;

public abstract class Error(string message, int code)
{
    public int Code { get;} = code;
    public string Message { get;} = message;
}