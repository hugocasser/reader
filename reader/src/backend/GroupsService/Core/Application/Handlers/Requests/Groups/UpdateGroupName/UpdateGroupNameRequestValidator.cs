using FluentValidation;

namespace Application.Handlers.Requests.Groups.UpdateGroupName;

public class UpdateGroupNameRequestValidator : AbstractValidator<UpdateGroupNameRequest>
{
    public UpdateGroupNameRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty().WithMessage("Group name must not be empty");
    }
}