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
public class NotesController(ISender sender) : ApiController(sender)
{
    [HttpGet]
    [Authorize]
    [Route("users/")]
    [Authorize]
    public async Task<IActionResult> GetAllUserNotes([FromRoute]PageSettingsRequestDto request, CancellationToken cancellationToken)
    {
        var command = new GetAllUserNotesQuery()
        {
            PageSettingsRequestDto = request
        };
        var requestResult = await Sender.Send(command, cancellationToken);
        
        return CustomObjectResult.FromResult(requestResult);
    }
    
    [HttpGet]
    [Authorize]
    [Route("groups/{groupId}/users/{userId}")]
    public async Task<IActionResult> GetAllUserNotesInGroup([FromQuery]GetAllUserNotesInGroupQuery request, CancellationToken cancellationToken)
    {
        var requestResult = await Sender.Send(request, cancellationToken);
        
        return CustomObjectResult.FromResult(requestResult);
    }
    
    [HttpGet]
    [Authorize]
    [Route($"{{noteId}}")]
    public async Task<IActionResult> GetNoteById([FromRoute]Guid noteId, CancellationToken cancellationToken)
    {
        var command = new GetNoteByIdQuery(noteId);
        var requestResult = await Sender.Send(command, cancellationToken);
        
        return CustomObjectResult.FromResult(requestResult);
    }
    
    [HttpGet]
    [Authorize]
    [Route("{groupId}")]
    public async Task<IActionResult> GetAllGroupNotes
        ([FromRoute]Guid groupId, [FromQuery]ReadingPageSettingsRequestDto pageSettingsRequestDto, CancellationToken cancellationToken)
    {
        var request = new GetAllGroupNotesQuery()
        {
            GroupId = groupId,
            PageSettingsRequestDto = pageSettingsRequestDto
        };
        var requestResult = await Sender.Send(request, cancellationToken);
        
        return CustomObjectResult.FromResult(requestResult);
    }
    
    [Authorize]
    [HttpPost]
    [Route("{noteId}")]
    public async Task<IActionResult> CreateNote([FromBody]CreateNoteCommand command, CancellationToken cancellationToken)
    {
        var requestResult = await Sender.Send(command, cancellationToken);
        
        return CustomObjectResult.FromResult(requestResult);
    }
    
    [HttpDelete]
    [Route("groups/{groupId}/notes/{noteId}")]
    public async Task<IActionResult> DeleteNote([FromRoute] Guid groupId, [FromRoute] Guid noteId, CancellationToken cancellationToken)
    {
        var request = new DeleteNoteCommand()
        {
            GroupId = groupId,
            NoteId = noteId
        };
        var requestResult = await Sender.Send(request, cancellationToken);
        
        return CustomObjectResult.FromResult(requestResult);
    }

    [HttpGet]
    [Route("groups/{groupId}/books/{bookId}")]
    [Authorize]
    public async Task<IActionResult> GetNotesByGroupIdAndBookIdAsync([FromRoute]Guid groupId, [FromRoute]Guid bookId,
        [FromQuery]ReadingPageSettingsRequestDto pageSettingsRequestDto, CancellationToken cancellationToken)
    {
        var request = new GetAllGroupBookNotesQuery(groupId, bookId, pageSettingsRequestDto);
        var requestResult = await Sender.Send(request, cancellationToken);
        
        return CustomObjectResult.FromResult(requestResult);
    }
}