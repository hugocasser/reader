using Application.Common;
using MediatR;

namespace Application.Requests.Commands.Books.RemoveBookFromUserReadingList;

public record RemoveBookFromUserCommandListRequest(Guid BookId, Guid GroupId, Guid RequestingUserId) : IRequest<Result<string>>;