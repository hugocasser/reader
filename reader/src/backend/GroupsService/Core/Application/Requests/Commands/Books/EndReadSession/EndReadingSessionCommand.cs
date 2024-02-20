using Application.Common;
using MediatR;

namespace Application.Requests.Commands.Books.EndReadSession;

public class EndReadingSessionCommand
    : IRequest<Result<string>>, IRequestWithRequestingUserId
{
    public Guid? RequestingUserId { get; set; }
    public Guid UserBookProgressId { get; init; }
    public int Progress { get; init; }
    public int LastReadSymbol { get; init; }
}