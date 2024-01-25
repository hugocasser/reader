using BusinessLogicLayer.Abstractions.Validation;
using BusinessLogicLayer.Exceptions;
using FluentValidation;

namespace BusinessLogicLayer.Validation;

public abstract class BaseValidationModel<T> : IBaseValidationModel
{
    public void Validate(object validator, IBaseValidationModel modelObj)
    {
        var instance = (IValidator<T>)validator;
        var result = instance.Validate((T)modelObj);

        if (result.IsValid || result.Errors.Count == 0) return;
        var errorMessages = result.Errors.Select(failure =>
                $"Property {failure.PropertyName}" + $" failed validation. Error was: {failure.ErrorMessage}\n")
            .Aggregate("", (current, errorMessage) => current + errorMessage);
        throw new BadRequestExceptionWithStatusCode(errorMessages);
    }
}