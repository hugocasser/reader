using Application.Abstractions.Repositories;
using Application.Common;
using MediatR;

namespace Application.Handlers.Requests.Books.RemoveBookFromUserReadingList;

public class RemoveBookFromUserReadingListRequestHandler
    (IUserBookProgressRepository _userBookProgressRepository)
    : IRequestHandler<RemoveBookFromUserReadingListRequest, Result<string>>
{
    public async Task<Result<string>> Handle(RemoveBookFromUserReadingListRequest request, CancellationToken cancellationToken)
    {
        var userBookProgress = await _userBookProgressRepository
            .GetProgressByUserIdBookIdAndGroupIdAsync(request.RequestingUserId, request.BookId, request.GroupId, cancellationToken);

        if (userBookProgress is null)
        {
            return new Result<string>(new Error("User book progress not found", 404));
        }   
        
        userBookProgress.Delete();
        await _userBookProgressRepository.DeleteByIdAsync(userBookProgress.Id, cancellationToken);
        await _userBookProgressRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<string>("Book removed from reading list");
    }
}