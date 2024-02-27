using Application.Common;

namespace Application.Abstractions;

public interface IResult
{
    public Error? Error { get; }
    public bool IsSuccess { get; }
    public string SerializeResponse();
}