using Application.Handlers.Queries.Groups.GetAllGroups;
using Application.Handlers.Queries.Groups.GetGroupById;
using Application.Requests.Commands.Groups.AddBookToGroup;
using Application.Requests.Commands.Groups.AddUserToGroup;
using Application.Requests.Commands.Groups.CreateGroup;
using Application.Requests.Commands.Groups.DeleteGroup;
using Application.Requests.Commands.Groups.RemoveBookFromGroup;
using Application.Requests.Commands.Groups.RemoveUserFromGroup;
using Application.Requests.Commands.Groups.UpdateGroupName;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controllers;

[Route("groups/")]
public class GroupController(ISender _sender) : ApiController(_sender)
{
    [HttpPost]
    public async Task<IActionResult> CreateGroup([FromBody]CreateGroupCommand command)
    {
        return Ok(await _sender.Send(command));
    }

    [HttpGet]
    [Route("{groupId}")]
    public async Task<IActionResult> GetGroup([FromQuery]GetGroupByIdQuery request)
    {
        return Ok(await _sender.Send(request));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGroups([FromQuery]GetAllGroupsQuery request)
    {
        return Ok(await _sender.Send(request));
    }

    [HttpPut]
    [Route("{groupId}/name")]
    public async Task<IActionResult> UpdateGroupName([FromBody]UpdateGroupNameCommand command)
    {
        return Ok(await _sender.Send(command));
    }
    
    [HttpPut]
    [Route("{groupId}/books")]
    public async Task<IActionResult> AddBookToGroup([FromBody]AddBookToGroupCommand command)
    {
        return Ok(await _sender.Send(command));
    }

    [HttpPut]
    [Route("{groupId}/books/rm")]
    public async Task<IActionResult> RemoveBookFromGroup([FromBody] RemoveBookFromGroupCommand command)
    {
        return Ok(await _sender.Send(command));
    }

    [HttpPut]
    [Route("{groupId}/users")]
    public async Task<IActionResult> AddUserToGroup([FromBody] AddUserToGroupCommand command)
    {
        return Ok(await _sender.Send(command));
    }

    [HttpPut]
    [Route("{groupId}/users/rm")]
    public async Task<IActionResult> RemoveUserFromGroup(RemoveUserFromGroupCommand command)
    {
        return Ok(await _sender.Send(command));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteGroup(DeleteGroupCommand command)
    {
        return Ok(await _sender.Send(command));
    }
}