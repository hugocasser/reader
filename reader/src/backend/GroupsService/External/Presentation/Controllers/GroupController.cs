using Application.Requests.Commands.Groups.AddBookToGroup;
using Application.Requests.Commands.Groups.AddUserToGroup;
using Application.Requests.Commands.Groups.CreateGroup;
using Application.Requests.Commands.Groups.DeleteGroup;
using Application.Requests.Commands.Groups.RemoveBookFromGroup;
using Application.Requests.Commands.Groups.RemoveUserFromGroup;
using Application.Requests.Commands.Groups.UpdateGroupName;
using Application.Requests.Queries.Groups.GetAllGroups;
using Application.Requests.Queries.Groups.GetGroupById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controllers;

[Route("groups/")]
public class GroupController(ISender sender) : ApiController(sender)
{
    [HttpPost]
    public async Task<IActionResult> CreateGroup([FromBody]CreateGroupCommand request)
    {
        return Ok(await sender.Send(request));
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
    public async Task<IActionResult> UpdateGroupName([FromBody]UpdateGroupNameCommand request)
    {
        return Ok(await _sender.Send(request));
    }
    
    [HttpPut]
    [Route("{groupId}/books")]
    public async Task<IActionResult> AddBookToGroup([FromBody]AddBookToGroupCommand request)
    {
        return Ok(await _sender.Send(request));
    }

    [HttpPut]
    [Route("{groupId}/books/rm")]
    public async Task<IActionResult> RemoveBookFromGroup([FromBody]RemoveBookFromGroupCommand request)
    {
        return Ok(await _sender.Send(request));
    }

    [HttpPut]
    [Route("{groupId}/users")]
    public async Task<IActionResult> AddUserToGroup([FromBody]AddUserToGroupCommand request)
    {
        return Ok(await _sender.Send(request));
    }

    [HttpPut]
    [Route("{groupId}/users/rm")]
    public async Task<IActionResult> RemoveUserFromGroup([FromBody]RemoveUserFromGroupCommand request)
    {
        return Ok(await _sender.Send(request));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteGroup([FromBody]DeleteGroupCommand request)
    {
        return Ok(await _sender.Send(request));
    }
}