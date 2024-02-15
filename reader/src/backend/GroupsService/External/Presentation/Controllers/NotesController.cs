using Application.Requests.Commands.Notes.CreateNote;
using Application.Requests.Commands.Notes.DeleteNote;
using Application.Requests.Queries.Notes.GetAllGroupNotes;
using Application.Requests.Queries.Notes.GetAllUserNotes;
using Application.Requests.Queries.Notes.GetAllUserNotesInGroup;
using Application.Requests.Queries.Notes.GetNoteById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controllers;

public class NotesController(ISender sender) : ApiController(sender)
{
    [HttpGet]
    public async Task<IActionResult> GetAllUserNotes([FromQuery]GetAllUserNotesQuery request)
    {
        return Ok(await _sender.Send(request));
    }
    
    [HttpGet]
    [Route("{groupId}/{userId}notes")]
    public async Task<IActionResult> GetAllUserNotesInGroup([FromQuery]GetAllUserNotesInGroupQuery request)
    {
        return Ok(await _sender.Send(request));
    }
    
    [HttpGet]
    [Route("{noteId}")]
    public async Task<IActionResult> GetNoteById([FromQuery]GetNoteByIdQuery request)
    {
        return Ok(await _sender.Send(request));
    }
    
    [HttpGet]
    [Route("{groupId}/notes")]
    public async Task<IActionResult> GetAllGroupNotes([FromQuery]GetAllGroupNotesQuery request)
    {
        return Ok(await _sender.Send(request));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateNote([FromBody]CreateNoteCommand command)
    {
        return Ok(await _sender.Send(command));
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteNote([FromBody]DeleteNoteCommand command)
    {
        return Ok(await _sender.Send(command));
    }
}