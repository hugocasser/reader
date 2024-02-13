using Application.Abstractions.Repositories;
using Application.Common;
using MediatR;

namespace Application.Handlers.Requests.Books.EndReadSession;

public class EndReadingSessionRequestHandler
    (IUserBookProgressRepository _userBookProgressRepository)
    : IRequestHandler<EndReadingSessionRequest, Result<string>>
{
    public async Task<Result<string>> Handle(EndReadingSessionRequest request, CancellationToken cancellationToken)
    {
        var userBookProgress = await _userBookProgressRepository.GetByIdAsync(request.UserBookProgressId, cancellationToken);
        
        if (userBookProgress is null)
        {
            return new Result<string>(new Error("User book progress not found", 404));
        }
        
        if (userBookProgress.UserId != request.RequestingUserId)
        {
            return new Result<string>(new Error("You can't end this reading session", 400));
        }

        if (userBookProgress.Progress > request.Progress)
        {
            return new Result<string>(new Error("Current progress can't be less than previous progress", 400));
        }

        if (request.Progress < request.LastReadSymbol)
        {
            return new Result<string>(new Error("Current progress can't be less than last read symbol", 400));
        }
        
        userBookProgress.UpdateUserBookProgress(request.Progress, request.LastReadSymbol);
        
        await _userBookProgressRepository.UpdateAsync(userBookProgress, cancellationToken);
        await _userBookProgressRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<string>("Session ended");
    }
}