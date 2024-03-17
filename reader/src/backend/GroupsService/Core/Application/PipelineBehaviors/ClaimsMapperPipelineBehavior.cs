using System.Security.Claims;
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
        if (_context.HttpContext == null)
        {
            return await next();
        }
        
        var stringId = _context.HttpContext.User.Identities.First().Claims.FirstOrDefault()?.Value; 
        var id = Guid.Parse(stringId ?? "00000000-0000-0000-0000-000000000000");
        request.SetRequestingUserId(id);

        return await next();
    }
}