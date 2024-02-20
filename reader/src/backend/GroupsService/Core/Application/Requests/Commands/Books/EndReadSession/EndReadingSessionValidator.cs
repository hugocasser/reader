using FluentValidation;

namespace Application.Requests.Commands.Books.EndReadSession;
public abstract class EndReadingSessionValidator
    : AbstractValidator<EndReadingSessionCommand>
{
    public EndReadingSessionValidator()
    {
        RuleFor(request => request.LastReadSymbol)
            .GreaterThan(0).WithMessage("Last read symbol must be greater than zero");
        
        RuleFor(request => request.Progress)
            .GreaterThan(0).WithMessage("Progress must be greater than zero");
    }
}