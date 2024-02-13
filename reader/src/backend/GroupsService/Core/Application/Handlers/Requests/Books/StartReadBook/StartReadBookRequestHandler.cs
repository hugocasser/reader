using Application.Abstractions.Repositories;
using Application.Common;
using Domain.Models;
using MediatR;

namespace Application.Handlers.Requests.Books.StartReadBook;

public class StartReadBookRequestHandler(IGroupsRepository _groupsRepository,
    IUserBookProgressRepository _userBookProgressRepository,
    IBooksRepository _booksRepository) : IRequestHandler<StartReadBookRequest, Result<string>>
{
    public async Task<Result<string>> Handle(StartReadBookRequest request, CancellationToken cancellationToken)
    {
        var group = await _groupsRepository.GetByIdAsync(request.GroupId, cancellationToken);

        if (group is null)
        {
            return new Result<string>(new Error("Group not found", 404));
        }

        var user = group.Members.FirstOrDefault(searchingUser => searchingUser.Id == request.RequestingUserId);
        
        if (user is null)
        {
            return new Result<string>(new Error("You are not a member of this group", 400));
        }

        var book = group.AllowedBooks.FirstOrDefault(searchingBook => searchingBook.Id == request.BookId);
        if (book is null)
        {
            return new Result<string>(new Error("Book isn't allowed in this group", 404));
        }
        
        var progressByUserIdAndBookId = await _userBookProgressRepository
            .GetProgressByUserIdBookIdAndGroupIdAsync(request.RequestingUserId, request.BookId, request.GroupId, cancellationToken);

        if (progressByUserIdAndBookId is not null)
        {
            return new Result<string>(new Error("You have already started reading this book", 400));
        }

        var progress = new UserBookProgress();
        progress.CreateUserBookProgress(book, group, user);
        
        await _userBookProgressRepository.CreateAsync(progress, cancellationToken);
        await _userBookProgressRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<string>(progress.Id.ToString());
    }
}