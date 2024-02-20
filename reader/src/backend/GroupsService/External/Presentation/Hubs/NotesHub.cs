using Application.Abstractions.Hubs;
using Application.Requests.Commands.Books.EndReadSession;
using Application.Requests.Commands.Notes.CreateNote;
using Application.Requests.Commands.Notes.DeleteNote;
using Application.Requests.Queries.Groups.GetGroupById;
using Application.Requests.Queries.Progress.GetProgressById;
using Application.Requests.Queries.Users.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Presentation.Exceptions;

namespace Presentation.Hubs;

public class NotesHub(ISender _sender , IHttpContextAccessor _httpContextAccessor) : Hub, INotesHub
{
    [Authorize]
    public async Task SendNoteAsync(CreateNoteCommand command)
    {
        var group = await _sender.Send(new GetGroupByIdQuery(command.GroupId));
        var result = await _sender.Send(command);
        
        if (group.IsSuccess && result.IsSuccess)
        {
            Clients.Group(group.Response.Id.ToString()).SendAsync("ReceiveNote", result.Response);
        }
    }

    [Authorize]
    public async Task DeleteNoteAsync(DeleteNoteCommand command)
    {
        var result = await _sender.Send(command);

        if (result.IsSuccess)
        {
            var group = await _sender.Send(new GetGroupByIdQuery(command.GroupId));

            if (group.IsSuccess)
            {
                Clients.Groups(group.Response.MembersDtos
                        .Select(x => x.Id.ToString()).ToArray())
                    .SendAsync("DeleteNote", result.Response);
            }
        }
        
    }

    public async Task SendUserProgressAsync(EndReadingSessionCommand command)
    {
        var result = await _sender.Send(command);
        var progress = await _sender.Send(new GetProgressByIdQuery(command.UserBookProgressId));

        if (result.IsSuccess && progress.IsSuccess)
        {
            await Clients.Group(progress.Response.Group.Id.ToString()).SendAsync("ReceiveUserProgress", progress.Response);
        }
    }

    [Authorize]
    public override async Task OnConnectedAsync()
    {
        var userId = Guid.Parse(Context.User.FindFirst("User-Id").Value);

        if (userId == Guid.Empty)
        {
            throw new NotValidClaimsException("user is not valid");
        }
        var user = await _sender.Send(new GetUserByIdQuery(userId));
            
        if (user.IsSuccess)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, user.Response.Group.ToString());
        }
            
        await base.OnConnectedAsync();
    }

    [Authorize]
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Guid.Parse(Context.User.FindFirst("User-Id").Value);

        if (userId == Guid.Empty)
        {
            throw new NotValidClaimsException("user is not valid");
        }
        
        var user = await _sender.Send(new GetUserByIdQuery(userId));
            
        if (user.IsSuccess)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, user.Response.Group.ToString());
        }
            
        await base.OnDisconnectedAsync(exception);

        throw new NotValidClaimsException("user is not valid");
    }
}