using Microsoft.AspNetCore.Mvc;
using IResult = Application.Abstractions.IResult;

namespace Presentation.Common;

public class CustomObjectResult : ObjectResult
{
    private CustomObjectResult(IResult result) : base(result.Error)
    {
        StatusCode = result.IsSuccess ? StatusCodes.Status200OK : result.Error.Code;
    }

    public static CustomObjectResult FromResult(IResult result)
    {
        return new CustomObjectResult(result);
    }
}