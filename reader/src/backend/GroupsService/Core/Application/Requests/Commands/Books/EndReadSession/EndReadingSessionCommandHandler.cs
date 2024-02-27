using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Common;
using Application.Results;
using Application.Results.Errors;
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
            return new Result<string>(new NotFoundError("progress"));
        }
        
        if (userBookProgress.UserId != command.RequestingUserId)
        {
            return new Result<string>(new BadRequestError("You can't end this reading session"));
        }

        if (userBookProgress.Progress > command.Progress)
        {
            return new Result<string>(new BadRequestError("Current progress can't be less than previous progress"));
        }
        
        userBookProgress.UpdateUserBookProgress(command.Progress, command.LastReadSymbol);
        
        await _userBookProgressRepository.UpdateAsync(userBookProgress, cancellationToken);
        await _userBookProgressRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<string>();
    }
}