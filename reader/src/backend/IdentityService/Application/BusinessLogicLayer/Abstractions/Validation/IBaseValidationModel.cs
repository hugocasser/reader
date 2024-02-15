namespace BusinessLogicLayer.Abstractions.Validation;

public interface IBaseValidationModel
{
    public void Validate(object validator, IBaseValidationModel modelObj);
}