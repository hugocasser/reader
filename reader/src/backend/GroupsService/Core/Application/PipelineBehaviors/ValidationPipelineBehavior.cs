using FluentValidation;
using MediatR;

namespace Application.PipelineBehaviors;

public class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> _validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var failures = _validators
            .Select(validator => validator.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(failure => failure != null)
            .ToArray();
        
        if (failures.Length != 0)
        {
            throw new ValidationException(failures);
        }
        
        return await next();
    }
}