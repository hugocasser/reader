using System.Net;
using System.Text.Json;
using Application.Exceptions;
using FluentValidation;

namespace Presentation.Middlewares;

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
            await HandleExceptionAsync(context, exception);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode code;
        string result;
        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.Errors);
                break;
            case ExceptionWithStatusCode exceptionWithStatusCode:
                code = (HttpStatusCode)exceptionWithStatusCode.StatusCode;
                result = JsonSerializer.Serialize(exceptionWithStatusCode.Message);
                break;
            default:
                code = HttpStatusCode.InternalServerError;
                result = JsonSerializer.Serialize(exception.Message);
                break;
                
        }
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
            
        return context.Response.WriteAsync(result);
    }
}