using MediatR;

namespace Application.Handlers.Requests.Books.StartReadBook;

public record StartReadBookRequest(Guid BookId, Guid UserId, Guid GroupId) : IRequest;