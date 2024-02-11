using Application.Abstractions.Repositories;
using Application.Common;
using Domain.Models;
using MediatR;

namespace Application.Handlers.Requests.Books.StartReadBook;

public class StartReadBookRequestHandler(IGroupsRepository _groupsRepository, IUserBookProgressRepository _userBookProgressRepository,
    IBooksRepository _booksRepository) : IRequestHandler<StartReadBookRequest, Result<string>>
{
    public async Task<Result<string>> Handle(StartReadBookRequest request, CancellationToken cancellationToken)
    {
        var group = await _groupsRepository.GetByIdAsync(request.GroupId, cancellationToken);

        if (group is null)
        {
            return new Result<string>(new Error("Group not found", 404));
        }

        
        if (group.Members.All(user => user.Id != request.RequestingUserId))
        {
            return new Result<string>(new Error("You are not a member of this group", 400));
        }

        if (group.AllowedBooks.All(book => book.Id != request.BookId))
        {
            return new Result<string>(new Error("Book isn't allowed in this group", 404));
        }
        
        var progressByUserIdAndBookId = await _userBookProgressRepository
            .GetProgressByUserIdBookIdAndGroupIdAsync(request.RequestingUserId, request.BookId, request.GroupId);

        if (progressByUserIdAndBookId is not null)
        {
            return new Result<string>(new Error("You have already started reading this book", 400));
        }

        var userBookProgress = new UserBookProgress
        {
            Id = Guid.NewGuid(),
            UserId = request.RequestingUserId,
            BookId = request.BookId,
            GroupId = request.GroupId,
            Group = group,
            User = group.Members.First(user => user.Id == request.RequestingUserId),
            Book = group.AllowedBooks.First(book => book.Id == request.BookId),
            Progress = 0,
            LastReadSymbol = 0
        };
        
        await _userBookProgressRepository.CreateAsync(userBookProgress, cancellationToken);
        
        return new Result<string>(userBookProgress.Id.ToString());
    }
}