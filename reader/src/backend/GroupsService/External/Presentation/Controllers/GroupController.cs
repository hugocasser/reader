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
    public async Task<IActionResult> CreateGroupAsync([FromBody]CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var groupResult = await Sender.Send(request, cancellationToken);
        
        return CustomObjectResult.FromResult(groupResult);
    }

    [HttpGet]
    [Route("{groupId}")]
    [Authorize]
    public async Task<IActionResult> GetGroupByIdAsync([FromRoute]Guid groupId, CancellationToken cancellationToken)
    {
        var request = new GetGroupByIdQuery(groupId);
        var requestResult = await Sender.Send(request, cancellationToken);

        return CustomObjectResult.FromResult(requestResult);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGroupsAsync([FromRoute]GetAllGroupsQuery request, CancellationToken cancellationToken)
    {
        var requestResult = await Sender.Send(request, cancellationToken);

        return CustomObjectResult.FromResult(requestResult);
    }

    [HttpPut]
    [Route("{groupId}/name/{newName}")]
    [Authorize]
    public async Task<IActionResult> UpdateGroupNameAsync([FromRoute] Guid groupId, [FromRoute] string newName, CancellationToken cancellationToken)
    {
        var request = new UpdateGroupNameCommand()
        {
            GroupId = groupId,
            Name = newName
        };
        var requestResult = await Sender.Send(request, cancellationToken);
        
        return CustomObjectResult.FromResult(requestResult);
    }
    
    [HttpPut]
    [Route("{groupId}/books")]
    [Authorize]
    public async Task<IActionResult> AddBookToGroupAsync([FromBody]AddBookToGroupCommand request, CancellationToken cancellationToken)
    {
        var requestResult = await Sender.Send(request, cancellationToken);
        
        return CustomObjectResult.FromResult(requestResult);
    }

    [HttpDelete]
    [Route("{groupId}/books/{bookId}")]
    [Authorize]
    public async Task<IActionResult> RemoveBookFromGroupAsync([FromRoute] Guid groupId, [FromRoute]Guid bookId, CancellationToken cancellationToken)
    {
        var request = new RemoveBookFromGroupCommand()
        {
            GroupId = groupId,
            BookId = bookId
        };
        var requestResult = await Sender.Send(request, cancellationToken);
        
        return CustomObjectResult.FromResult(requestResult);
    }

    [HttpPut]
    [Route("{groupId}/users")]
    [Authorize]
    public async Task<IActionResult> AddUserToGroupAsync([FromBody]AddUserToGroupCommand request, CancellationToken cancellationToken)
    {
        var requestResult = await Sender.Send(request, cancellationToken);
        
        return CustomObjectResult.FromResult(requestResult);
    }

    [HttpDelete]
    [Route("{groupId}/users/{userId}")]
    [Authorize]
    public async Task<IActionResult> RemoveUserFromGroupAsync([FromRoute] Guid groupId, [FromRoute]Guid userId, CancellationToken cancellationToken)
    {
        var request = new RemoveUserFromGroupCommand()
        {
            GroupId = groupId,
            UserToRemoveId = userId
        };
        var requestResult = await Sender.Send(request, cancellationToken);
        
        return CustomObjectResult.FromResult(requestResult);
    }

    [HttpDelete]
    [Route("{groupId}")]
    public async Task<IActionResult> DeleteGroupAsync([FromRoute]Guid groupId, CancellationToken cancellationToken)
    {
        var request = new DeleteGroupCommand()
        {
            GroupId = groupId
        };
        var requestResult = await Sender.Send(request, cancellationToken);
        
        return CustomObjectResult.FromResult(requestResult);
    }
}