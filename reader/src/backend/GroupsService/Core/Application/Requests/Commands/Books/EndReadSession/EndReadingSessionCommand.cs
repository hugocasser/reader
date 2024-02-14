using Application.Common;
using MediatR;

namespace Application.Requests.Commands.Books.EndReadSession;

public record EndReadingSessionCommand(Guid UserBookProgressId, int Progress, int LastReadSymbol, Guid RequestingUserId) : IRequest<Result<string>>;