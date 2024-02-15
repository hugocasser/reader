using BusinessLogicLayer.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

public sealed class ErrorController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/error")]
    public IActionResult HandleError([FromServices] IHostEnvironment hostEnvironment)
    {
        if (!hostEnvironment.IsDevelopment())
        {
            return NotFound();
        }
        
        var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
        var title = exceptionHandlerFeature.Error.Message;
        var statusCode = StatusCodes.Status500InternalServerError;

        if (exceptionHandlerFeature.Error is not ExceptionWithStatusCode httpResponseException)
            return Problem(
                detail: exceptionHandlerFeature.Error.StackTrace,
                title: title,
                statusCode: statusCode);
        title = httpResponseException.ErrorMessage;
        statusCode = httpResponseException.StatusCode;


        return Problem(
            detail: exceptionHandlerFeature.Error.StackTrace,
            title: title,
            statusCode: statusCode);
    }
}