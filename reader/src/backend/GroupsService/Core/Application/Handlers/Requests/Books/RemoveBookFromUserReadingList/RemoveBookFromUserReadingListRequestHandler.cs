using Application.Abstractions.Repositories;
using Application.Exceptions;
using MediatR;

namespace Application.Handlers.Requests.Books.RemoveBookFromUserReadingList;

public class RemoveBookFromUserReadingListRequestHandler(IUserBookProgressRepository userBookProgressRepository) : IRequestHandler<RemoveBookFromUserReadingListRequest>
{
    public async Task Handle(RemoveBookFromUserReadingListRequest request, CancellationToken cancellationToken)
    {
        var userBookProgress = await userBookProgressRepository
            .GetProgressByUserIdBookIdAndGroupIdAsync(request.UserId, request.BookId, request.GroupId)
            ?? throw new NotFoundException("You have not started reading this book in this group");

        await userBookProgressRepository.DeleteProgressAsync(userBookProgress);
    }
}