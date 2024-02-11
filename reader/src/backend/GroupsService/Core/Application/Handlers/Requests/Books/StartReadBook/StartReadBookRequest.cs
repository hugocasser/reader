using Application.Common;
using MediatR;

namespace Application.Handlers.Requests.Books.StartReadBook;

public record StartReadBookRequest(Guid BookId, Guid GroupId, Guid RequestingUserId) : IRequest<Result<string>>;