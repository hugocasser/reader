using System.Net;
using System.Text.Json;
using BusinessLogicLayer.Exceptions;
using FluentValidation;

namespace PresentationLayer.Middleware;

public class CustomExceptionHandler(RequestDelegate next)
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
            case NotFoundException:
                code = HttpStatusCode.NotFound;
                result = JsonSerializer.Serialize(exception.Message);
                break;
            case IdentityException:
                code = HttpStatusCode.Unauthorized;
                result = JsonSerializer.Serialize(exception.Message);
                break;
            case BadRequestException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(exception.Message);
                break;
            case EmailNotConfirmedException:
                code = HttpStatusCode.Unauthorized;
                result = JsonSerializer.Serialize(exception.Message);
                break;
            case IncorrectEmailOrPasswordException:
                code = HttpStatusCode.Unauthorized;
                result = JsonSerializer.Serialize(exception.Message);
                break;
            case EmailNotSentException:
                code = HttpStatusCode.BadGateway;
                result = JsonSerializer.Serialize(exception.Message);
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