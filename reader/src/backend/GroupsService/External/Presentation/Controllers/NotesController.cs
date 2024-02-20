using Application.Abstractions.Repositories;
using Application.Dtos.Requests;
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

namespace Presentation.Controllers;
[Route("notes/")]
public class NotesController(ISender sender, IBooksRepository _booksRepository) : ApiController(sender)
{
    [HttpGet]
    [Route("{userId}/notes")]
    [Authorize]
    public async Task<IActionResult> GetAllUserNotes([FromQuery]GetAllUserNotesQuery request)
    {
        return Ok(await _sender.Send(request));
    }
    
    [HttpGet]
    [Authorize]
    [Route("{groupId}/users/{userId}")]
    public async Task<IActionResult> GetAllUserNotesInGroup([FromQuery]GetAllUserNotesInGroupQuery request)
    {
        return Ok(await _sender.Send(request));
    }
    
    [HttpGet]
    [Authorize]
    [Route("id/{noteId}")]
    public async Task<IActionResult> GetNoteById([FromRoute]GetNoteByIdQuery request)
    {
        return Ok(await _sender.Send(request));
    }
    
    [HttpGet]
    [Authorize]
    [Route("{groupId}")]
    public async Task<IActionResult> GetAllGroupNotes([FromRoute]GetAllGroupNotesQuery request)
    {
        return Ok(await _sender.Send(request));
    }
    
    [Authorize]
    [HttpPost]
    [Route("{noteId}")]
    public async Task<IActionResult> CreateNote([FromBody]CreateNoteCommand command)
    {
        return Ok(await _sender.Send(command));
    }
    
    [HttpDelete]
    [Route("{noteId}")]
    public async Task<IActionResult> DeleteNote([FromBody]DeleteNoteCommand command)
    {
        return Ok(await _sender.Send(command));
    }

    [HttpGet]
    [Route("{groupId}/books/{bookId}")]
    [Authorize]
    public async Task<IActionResult> GetNotesByGroupIdAndBookIdAsync(Guid groupId, Guid bookId,
        [FromRoute]ReadingPageSettingsRequestDto pageSettingsRequestDto)
    {
        return Ok(await _sender.Send(new GetAllGroupBookNotesQuery(groupId, bookId, pageSettingsRequestDto)));
    }
}