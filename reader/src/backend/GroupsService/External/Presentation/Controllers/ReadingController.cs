using Application.Requests.Commands.Books.StartReadBook;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controllers;

[Route("reading")]
public class ReadingController(ISender sender) : ApiController(sender)
{
    [HttpPost]
    [Route("{bookId}/start")]
    public async Task<IActionResult> StartReading(StartReadingBookCommand command)
    {
        return Ok(await _sender.Send(command));
    }
    
    
}