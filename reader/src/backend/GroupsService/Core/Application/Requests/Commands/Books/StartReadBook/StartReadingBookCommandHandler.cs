using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Common;
using Application.Results;
using Application.Results.Errors;
using Domain.Models;
using MediatR;

namespace Application.Requests.Commands.Books.StartReadBook;

public class StartReadingBookCommandHandler(IGroupsRepository _groupsRepository,
    IUserBookProgressRepository _userBookProgressRepository,
    IBooksRepository _booksRepository) : IRequestHandler<StartReadingBookCommand, Result<string>>
{
    public async Task<Result<string>> Handle(StartReadingBookCommand command, CancellationToken cancellationToken)
    {
        var group = await _groupsRepository.GetByIdAsync(command.GroupId, cancellationToken);

        if (group is null)
        {
            return new Result<string>(new NotFoundError("Group"));
        }

        var user = group.Members.FirstOrDefault(searchingUser => searchingUser.Id == command.RequestingUserId);
        
        if (user is null)
        {
            return new Result<string>(new BadRequestError("You aren't member of this group"));
        }

        var book = group.AllowedBooks.FirstOrDefault(searchingBook => searchingBook.Id == command.BookId);
        if (book is null)
        {
            return new Result<string>(new BadRequestError("Book is not allowed in this group"));
        }
        
        var progressByUserIdAndBookId = await _userBookProgressRepository
            .GetProgressByUserIdBookIdAndGroupIdAsync(command.RequestingUserId ?? Guid.Empty, command.BookId, command.GroupId, cancellationToken);

        if (progressByUserIdAndBookId is not null)
        {
            return new Result<string>(new BadRequestError("You have already started reading this book"));
        }

        var progress = new UserBookProgress();
        progress.CreateUserBookProgress(book, group, user);
        
        await _userBookProgressRepository.CreateAsync(progress, cancellationToken);
        await _userBookProgressRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<string>(progress.Id.ToString());
    }
}