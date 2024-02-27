using Application.Abstractions;
using Application.Common;
using Application.Results;
using MediatR;

namespace Application.Requests.Commands.Books.StartReadBook;

public class StartReadingBookCommand
    : IRequest<Result<string>>, IRequestWithRequestingUserId
{
    public Guid? RequestingUserId { get; set; }
    public Guid BookId { get; init; }
    public Guid GroupId { get; init; }
}