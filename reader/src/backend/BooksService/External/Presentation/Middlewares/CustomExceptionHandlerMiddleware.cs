using System.Net;
using System.Text.Json;
using Application.Exceptions;
using FluentValidation;

namespace Presentation.Middlewares;

public class CustomExceptionHandlerMiddleware(RequestDelegate _next)
{
     public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        string result;
        
        switch (exception)
        {
            case ValidationException validationException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.Errors);
                break;
            case ExceptionWithStatusCode exceptionWithStatusCode:
                context.Response.StatusCode= exceptionWithStatusCode.StatusCode;
                result = JsonSerializer.Serialize(exceptionWithStatusCode.Message);
                break;
            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                result = JsonSerializer.Serialize(exception.Message);
                break;
        }
        
        context.Response.ContentType = "application/json";
            
        return context.Response.WriteAsync(result);
    }
}