namespace Application.Abstractions;

public interface IRequest
{
    public void Validate(object validator, IRequest request);
}