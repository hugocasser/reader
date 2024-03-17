using System.Text.Json.Serialization;

namespace Application.Abstractions;

public abstract class Error(string message, int code)
{
    [JsonIgnore]
    public int Code { get;} = code;
    public string Message { get;} = message;
}