using Application.Requests.Commands.Books.StartReadBook;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using Presentation.Common;

namespace Presentation.Controllers;

[Route("reading/")]
public class ReadingController(ISender sender) : ApiController(sender)
{
    [HttpPost]
    [Route("groups/{groupId}/books/{bookId}")]
    public async Task<IActionResult> StartReading([FromRoute]Guid groupId, [FromRoute]Guid bookId, CancellationToken cancellationToken)
    {
        var request = new StartReadingBookCommand()
        {
            GroupId = groupId,
            BookId = bookId
        };
        var requestResult = await Sender.Send(request, cancellationToken);
        
        return CustomObjectResult.FromResult(requestResult);
    }
    
    
}