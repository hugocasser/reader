using Application.Requests.Commands.Books.StartReadBook;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controllers;

public class ReadingController(ISender _sender) : ApiController(_sender)
{
    [HttpPost]
    [Route("{bookId}/start")]
    public async Task<IActionResult> StartReading(StartReadingBookCommand command)
    {
        return Ok(await _sender.Send(command));
    }
    
    
}