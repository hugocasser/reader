using FluentValidation;

namespace Application.Handlers.Requests.Books.EndReadSession;

public class EndReadingSessionValidator : AbstractValidator<EndReadingSessionRequest>
{
    public EndReadingSessionValidator()
    {
        RuleFor(session => session.Progress)
            .GreaterThan(0).WithMessage("Progress must be greater than 0")
            .LessThan(0).WithMessage("Progress must be less than 0");
        
        RuleFor(session => session.LastReadSymbol)
            .GreaterThan(0).WithMessage("Last read symbol must be greater than 0");
    }
}