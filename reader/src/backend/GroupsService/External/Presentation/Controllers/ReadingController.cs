using Application.Requests.Commands.Books.StartReadBook;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using Presentation.Common;

namespace Presentation.Controllers;

[Route("reading")]
public class ReadingController(ISender sender) : ApiController(sender)
{
    [HttpPost]
    [Route("{bookId}/start")]
    public async Task<IActionResult> StartReading([FromBody]StartReadingBookCommand command)
    {
        var requestResult = await _sender.Send(command);
        
        return CustomObjectResult.FromResult(requestResult);
    }
    
    
}