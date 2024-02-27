using Application.Abstractions;
using Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.PipelineBehaviors;

public class ClaimsMapperPipelineBehavior<TRequest, TResponse>(IHttpContextAccessor _context) 
    : IPipelineBehavior<TRequest, TResponse> where TRequest : class, IRequestWithRequestingUserId
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_context.HttpContext?.User == null)
        {
            return await next();
        }
        
        var id = Guid.Parse(_context.HttpContext.User.FindFirst("User-Id")?.Value ?? "00000000-0000-0000-0000-000000000000");
        request.SetRequestingUserId(id);

        return await next();
    }
}