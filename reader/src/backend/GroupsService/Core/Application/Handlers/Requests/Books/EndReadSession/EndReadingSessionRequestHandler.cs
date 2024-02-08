using Application.Abstractions.Repositories;
using Application.Exceptions;
using Domain.Models;
using MediatR;

namespace Application.Handlers.Requests.Books.EndReadSession;

public class EndReadingSessionRequestHandler(IUserBookProgressRepository userBookProgressRepository) : IRequestHandler<EndReadingSessionRequest>
{
    public async Task Handle(EndReadingSessionRequest request, CancellationToken cancellationToken)
    {
        var userBookProgress = await userBookProgressRepository.GetProgressByIdAsync(request.UserBookProgressId);

        if (userBookProgress is null)
        {
            throw new NotFoundException("User book progress not found");
        }

        if (userBookProgress.Progress < request.Progress)
        {
            throw new BadRequestException("Progress can't be less than previous");
        }

        if (request.Progress < request.LastReadSymbol)
        {
            throw new BadRequestException("Progress can't be less than last read symbol");
        }
        
        userBookProgress.Progress = request.Progress;
        userBookProgress.LastReadSymbol = request.LastReadSymbol;
        
        await userBookProgressRepository.UpdateProgressAsync(userBookProgress);
        await userBookProgressRepository.SaveChangesAsync();
    }
}