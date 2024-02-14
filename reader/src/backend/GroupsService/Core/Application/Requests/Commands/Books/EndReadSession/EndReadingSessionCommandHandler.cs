using Application.Abstractions.Repositories;
using Application.Common;
using MediatR;

namespace Application.Requests.Commands.Books.EndReadSession;

public class EndReadingSessionCommandHandler
    (IUserBookProgressRepository _userBookProgressRepository)
    : IRequestHandler<EndReadingSessionCommand, Result<string>>
{
    public async Task<Result<string>> Handle(EndReadingSessionCommand command, CancellationToken cancellationToken)
    {
        var userBookProgress = await _userBookProgressRepository.GetByIdAsync(command.UserBookProgressId, cancellationToken);
        
        if (userBookProgress is null)
        {
            return new Result<string>(new Error("User book progress not found", 404));
        }
        
        if (userBookProgress.UserId != command.RequestingUserId)
        {
            return new Result<string>(new Error("You can't end this reading session", 400));
        }

        if (userBookProgress.Progress > command.Progress)
        {
            return new Result<string>(new Error("Current progress can't be less than previous progress", 400));
        }

        if (command.Progress < command.LastReadSymbol)
        {
            return new Result<string>(new Error("Current progress can't be less than last read symbol", 400));
        }
        
        userBookProgress.UpdateUserBookProgress(command.Progress, command.LastReadSymbol);
        
        await _userBookProgressRepository.UpdateAsync(userBookProgress, cancellationToken);
        await _userBookProgressRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<string>("Session ended");
    }
}