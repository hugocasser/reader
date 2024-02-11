using Application.Common;
using MediatR;

namespace Application.Handlers.Requests.Books.RemoveBookFromUserReadingList;

public record RemoveBookFromUserReadingListRequest(Guid BookId, Guid GroupId, Guid RequestingUserId) : IRequest<Result<string>>;