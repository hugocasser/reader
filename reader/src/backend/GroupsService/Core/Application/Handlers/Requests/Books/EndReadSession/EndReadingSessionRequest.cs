using Application.Common;
using MediatR;

namespace Application.Handlers.Requests.Books.EndReadSession;

public record EndReadingSessionRequest(Guid UserBookProgressId, int Progress, int LastReadSymbol, Guid RequestingUserId) : IRequest<Result<string>>;