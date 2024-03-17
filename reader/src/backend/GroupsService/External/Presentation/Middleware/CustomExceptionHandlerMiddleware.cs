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
                    if (context.Response.HasStarted)
                        break;
                    context.Response.StatusCode = claimsException.Code;
                    await context.Response.WriteAsync(claimsException.Message);
                    break;
                }
                default:
                {
                    if (!context.Response.HasStarted)
                        context.Response.StatusCode = 500;
                    await context.Response.WriteAsync(exception.Message);
                    break;
                }
            }
        }
    }
}