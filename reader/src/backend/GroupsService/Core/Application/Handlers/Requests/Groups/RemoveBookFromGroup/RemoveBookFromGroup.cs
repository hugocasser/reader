using MediatR;

namespace Application.Handlers.Requests.Groups.RemoveBookFromGroup;

public record RemoveBookFromGroupRequest(Guid BookId, Guid GroupId, Guid UserId) : IRequest;