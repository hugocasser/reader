using Application.Common;
using MediatR;

namespace Application.Requests.Commands.Books.StartReadBook;

public record StartReadingBookCommand(Guid BookId, Guid GroupId, Guid RequestingUserId) : IRequest<Result<string>>;