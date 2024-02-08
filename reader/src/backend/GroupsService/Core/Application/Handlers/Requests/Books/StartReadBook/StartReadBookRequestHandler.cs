using Application.Abstractions.Repositories;
using Application.Exceptions;
using Domain.Models;
using MediatR;

namespace Application.Handlers.Requests.Books.StartReadBook;

public class StartReadBookRequestHandler(IGroupsRepository groupsRepository, IUserBookProgressRepository userBookProgressRepository,
    IBooksRepository booksRepository) : IRequestHandler<StartReadBookRequest>
{
    public async Task Handle(StartReadBookRequest request, CancellationToken cancellationToken)
    {
        var group = await groupsRepository.GetGroupByIdAsync(request.GroupId);

        if (group is null)
        {
            throw new NotFoundException("Group not found");
        }

        if (group.Members.All(user => user.Id != request.UserId))
        {
            throw new BadRequestException("You are not a member of this group");
        }

        if (group.AllowedBooks.All(book => book.Id != request.BookId))
        {
            throw new BadRequestException("Book is not allowed in this group");
        }
        
        var progressByUserIdAndBookId = await userBookProgressRepository
            .GetProgressByUserIdBookIdAndGroupIdAsync(request.UserId, request.BookId, request.GroupId);

        if (progressByUserIdAndBookId is not null)
        {
            throw new BadRequestException("You have already started reading this book in this group");
        }

        var userBookProgress = new UserBookProgress
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            BookId = request.BookId,
            GroupId = request.GroupId,
            Group = group,
            User = group.Members.First(user => user.Id == request.UserId),
            Book = group.AllowedBooks.First(book => book.Id == request.BookId),
            Progress = 0,
            LastReadSymbol = 0
        };
    }
}