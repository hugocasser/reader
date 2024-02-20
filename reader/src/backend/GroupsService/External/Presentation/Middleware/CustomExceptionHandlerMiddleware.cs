using FluentValidation;
using Presentation.Exceptions;

namespace Presentation.Middleware;

public class CustomExceptionHandlerMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            switch (exception)
            {
                case ValidationException validationException:
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync(validationException.Message);
                    break;
                }
                case NotValidClaimsException claimsException:
                {
                    context.Response.StatusCode = claimsException.Code;
                    await context.Response.WriteAsync(claimsException.Message);
                    break;
                }
                default:
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync(exception.Message);
                    break;
                }
            }
        }
    }
}