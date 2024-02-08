using FluentValidation;

namespace Application.Handlers.Requests.Groups.CreateGroup;

public class CreateGroupRequestValidator : AbstractValidator<CreateGroupRequest>
{
    public CreateGroupRequestValidator()
    {
        RuleFor(request => request.GroupName)
            .NotEmpty().WithMessage("Group name must not be empty");
    }
}