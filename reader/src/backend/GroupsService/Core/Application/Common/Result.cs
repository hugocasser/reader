using System.Text.Json;

namespace Application.Common;

public class Result<T> : IResult where T : class
{
    public Error? Error { get; }
    public bool IsSuccess { get; }
    public T? Response { get; }
    
    public string SerializeResponse()
    {
        return IsSuccess ? JsonSerializer.Serialize(Response)
            : JsonSerializer.Serialize(Error.Message);
    }

    public Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    public Result(T response)
    {
        IsSuccess = true;
        Response = response;
    }
}