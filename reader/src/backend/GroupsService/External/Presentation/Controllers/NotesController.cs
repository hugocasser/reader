using Application.Abstractions.Repositories;
using Application.Dtos.Requests;
using Application.Dtos.Views;
using Application.Requests.Commands.Notes.CreateNote;
using Application.Requests.Commands.Notes.DeleteNote;
using Application.Requests.Queries.Notes.GetAllGroupBookNotes;
using Application.Requests.Queries.Notes.GetAllGroupNotes;
using Application.Requests.Queries.Notes.GetAllUserNotes;
using Application.Requests.Queries.Notes.GetAllUserNotesInGroup;
using Application.Requests.Queries.Notes.GetNoteById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using Presentation.Common;

namespace Presentation.Controllers;
[Route("notes/")]
public class NotesController(ISender sender, IBooksRepository _booksRepository) : ApiController(sender)
{
    [HttpGet]
    [Route($"{{userId}}")]
    [Authorize]
    public async Task<IActionResult> GetAllUserNotes([FromQuery]PageSettingsRequestDto request)
    {
        var command = new GetAllUserNotesQuery()
        {
            PageSettingsRequestDto = request
        };
        
        var requestResult = await Sender.Send(command);
        
        return CustomObjectResult.FromResult(requestResult);
    }
    
    [HttpGet]
    [Authorize]
    [Route("{groupId}/users/{userId}")]
    public async Task<IActionResult> GetAllUserNotesInGroup([FromQuery]GetAllUserNotesInGroupQuery request)
    {
        var requestResult = await Sender.Send(request);
        
        return CustomObjectResult.FromResult(requestResult);
    }
    
    [HttpGet]
    [Authorize]
    [Route($"{{noteId}}")]
    public async Task<IActionResult> GetNoteById([FromRoute]Guid noteId)
    {
        var command = new GetNoteByIdQuery(noteId);
        var requestResult = await Sender.Send(command);
        
        return CustomObjectResult.FromResult(requestResult);
    }
    
    [HttpGet]
    [Authorize]
    [Route("{groupId}")]
    public async Task<IActionResult> GetAllGroupNotes([FromQuery]GetAllGroupNotesQuery request)
    {
        var requestResult = await Sender.Send(request);
        
        return CustomObjectResult.FromResult(requestResult);
    }
    
    [Authorize]
    [HttpPost]
    [Route("{noteId}")]
    public async Task<IActionResult> CreateNote([FromBody]CreateNoteCommand command)
    {
        var requestResult = await Sender.Send(command);
        
        return CustomObjectResult.FromResult(requestResult);
    }
    
    [HttpDelete]
    [Route("{noteId}")]
    public async Task<IActionResult> DeleteNote([FromBody]DeleteNoteCommand command)
    {
        var requestResult = await Sender.Send(command);
        
        return CustomObjectResult.FromResult(requestResult);
    }

    [HttpGet]
    [Route("{groupId}/books/{bookId}")]
    [Authorize]
    public async Task<IActionResult> GetNotesByGroupIdAndBookIdAsync([FromRoute]Guid groupId, [FromRoute]Guid bookId,
        [FromQuery]ReadingPageSettingsRequestDto pageSettingsRequestDto)
    {
        var request = new GetAllGroupBookNotesQuery(groupId, bookId, pageSettingsRequestDto);
        var requestResult = await Sender.Send(request);
        
        return CustomObjectResult.FromResult(requestResult);
    }
}