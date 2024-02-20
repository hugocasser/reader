using FluentValidation;

namespace Application.Requests.Commands.Groups.UpdateGroupName;

public class UpdateGroupNameValidator : AbstractValidator<UpdateGroupNameCommand>
{
    public UpdateGroupNameValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty().WithMessage("Group name must not be empty");
    }
}