using MediatR;

namespace Application.Handlers.Requests.Books.RemoveBookFromUserReadingList;

public record RemoveBookFromUserReadingListRequest(Guid UserId, Guid BookId, Guid GroupId) : IRequest;