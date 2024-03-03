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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using Presentation.Common;

namespace Presentation.Controllers;

[Route("groups/")]
public class GroupController(ISender sender) : ApiController(sender)
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateGroupAsync([FromBody]CreateGroupCommand request)
    {
        var groupResult = await Sender.Send(request);
        
        return CustomObjectResult.FromResult(groupResult);
    }

    [HttpGet]
    [Route("{groupId}")]
    [Authorize]
    public async Task<IActionResult> GetGroupByIdAsync([FromQuery]GetGroupByIdQuery request)
    {
        var requestResult = await Sender.Send(request);

        return CustomObjectResult.FromResult(requestResult);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGroupsAsync([FromRoute]GetAllGroupsQuery request)
    {
        var requestResult = await Sender.Send(request);

        return CustomObjectResult.FromResult(requestResult);
    }

    [HttpPut]
    [Route("{groupId}/name")]
    [Authorize]
    public async Task<IActionResult> UpdateGroupNameAsync([FromBody]UpdateGroupNameCommand request)
    {
        var requestResult = await Sender.Send(request);
        
        return CustomObjectResult.FromResult(requestResult);
    }
    
    [HttpPut]
    [Route("{groupId}/books")]
    [Authorize]
    public async Task<IActionResult> AddBookToGroupAsync([FromBody]AddBookToGroupCommand request)
    {
        var requestResult = await Sender.Send(request);
        
        return CustomObjectResult.FromResult(requestResult);
    }

    [HttpDelete]
    [Route("{groupId}/books")]
    [Authorize]
    public async Task<IActionResult> RemoveBookFromGroupAsync([FromBody]RemoveBookFromGroupCommand request)
    {
        var requestResult = await Sender.Send(request);
        
        return CustomObjectResult.FromResult(requestResult);
    }

    [HttpPut]
    [Route("{groupId}/users")]
    [Authorize]
    public async Task<IActionResult> AddUserToGroupAsync([FromBody]AddUserToGroupCommand request)
    {
        var requestResult = await Sender.Send(request);
        
        return CustomObjectResult.FromResult(requestResult);
    }

    [HttpDelete]
    [Route("{groupId}/users")]
    [Authorize]
    public async Task<IActionResult> RemoveUserFromGroupAsync([FromBody]RemoveUserFromGroupCommand request)
    {
        var requestResult = await Sender.Send(request);
        
        return CustomObjectResult.FromResult(requestResult);
    }

    [HttpDelete]
    [Route("{groupId}")]
    public async Task<IActionResult> DeleteGroupAsync([FromBody]DeleteGroupCommand request)
    {
        var requestResult = await Sender.Send(request);
        
        return CustomObjectResult.FromResult(requestResult);
    }
}