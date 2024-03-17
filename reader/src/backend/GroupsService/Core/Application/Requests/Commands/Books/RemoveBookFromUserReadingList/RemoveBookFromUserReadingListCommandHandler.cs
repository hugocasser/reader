using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Common;
using Application.Results;
using Application.Results.Errors;
using Domain.DomainEvents.UserProgresses;
using MediatR;

namespace Application.Requests.Commands.Books.RemoveBookFromUserReadingList;

public class RemoveBookFromUserReadingListCommandHandler
    (IUserBookProgressRepository _userBookProgressRepository)
    : IRequestHandler<RemoveBookFromUserCommandListRequest, Result<string>>
{
    public async Task<Result<string>> Handle(RemoveBookFromUserCommandListRequest request, CancellationToken cancellationToken)
    {
        var userBookProgresses = await _userBookProgressRepository
            .GetByAsync(userBookProgress =>
                    userBookProgress.BookId == request.BookId
                    && userBookProgress.UserId == request.RequestingUserId
                    && userBookProgress.GroupId == request.GroupId,
                cancellationToken);

        var progress = userBookProgresses.First();
        
        if (progress is null)
        {
            return new Result<string>(new NotFoundError("Progress"));
        }   
        
        progress.Delete(new UserBookProgressDeletedEvent(progress.Id));
        await _userBookProgressRepository.DeleteByIdAsync(progress.Id, cancellationToken);
        await _userBookProgressRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<string>();
    }
}