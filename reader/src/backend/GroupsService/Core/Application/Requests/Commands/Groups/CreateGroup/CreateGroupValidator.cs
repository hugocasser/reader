using FluentValidation;

namespace Application.Requests.Commands.Groups.CreateGroup;

public class CreateGroupValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupValidator()
    {
        RuleFor(request => request.GroupName)
            .NotEmpty().WithMessage("Group name must not be empty");
    }
}