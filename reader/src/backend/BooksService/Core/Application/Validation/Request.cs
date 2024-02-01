using Application.Abstractions;
using Application.Exceptions;
using FluentValidation;

namespace Application.Validation;

public class Request<T> : IRequest where T : class
{
    public void Validate(object validator, IRequest request)
    {
        var instance = (IValidator<T>)validator;
        var result = instance.Validate((T)request);

        if (result.IsValid || result.Errors.Count == 0) return;
        var errorMessages = result.Errors.Select(failure =>
                $"Property {failure.PropertyName}" + $" failed validation. Error was: {failure.ErrorMessage}\n")
            .Aggregate("", (current, errorMessage) => current + errorMessage);
        throw new BadRequestExceptionWithStatusCode(errorMessages);
    }
}