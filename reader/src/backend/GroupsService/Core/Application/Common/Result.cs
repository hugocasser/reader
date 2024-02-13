namespace Application.Common;

public class Result<T> where T : class
{
    public Error? Error { get; }
    public bool IsSuccess { get; }
    public T? Response { get; }

    public Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    public Result(T response)
    {
        Error = new Error("Ok", 200);
        IsSuccess = true;
        Response = response;
    }
}