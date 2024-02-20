using Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using IResult = Application.Common.IResult;

namespace Application.PipelineBehaviors;

public class ErrorsPipelineBehaviour<TRequest, TResponse>(IHttpContextAccessor _context)
    : IPipelineBehavior<TRequest, TResponse> where TResponse : IResult where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var result = await next();

        if (_context.HttpContext == null)
        {
            return result;
        }
        
        if (_context.HttpContext.Response.HasStarted)
        {
            return result;
        }

        _context.HttpContext.Response.ContentType = "application/json";
        _context.HttpContext.Response.StatusCode = result.IsSuccess ? 200 : result.Error.Code;
        await _context.HttpContext.Response.WriteAsync(result.SerializeResponse(), cancellationToken);
        
        return result;
    }
    
}