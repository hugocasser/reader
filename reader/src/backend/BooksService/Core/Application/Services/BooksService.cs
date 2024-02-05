using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Dtos.Requests;
using Application.Dtos.Requests.Books;
using Application.Dtos.Views.Books;
using Application.Exceptions;
using Domain.Models;

namespace Application.Services;

public class BooksService(
    IBooksRepository booksRepository,
    IAuthorsRepository authorsRepository,
    ICategoriesRepository categoriesRepository) : IBooksService
{
    public async Task CreateBookAsync(CreateBookRequest request, CancellationToken cancellationToken)
    {
        if (!await authorsRepository.AuthorExistsAsync(request.AuthorId, cancellationToken))
        {
            throw new BadRequestException("Author with this id not found");
        }

        if (await categoriesRepository.CategoryExistsAsync(request.CategoryId, cancellationToken))
        {
            throw new BadRequestException("Category with this id not found");
        }

        var bookModel = new Book
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Text = request.Text,
            AuthorId = request.AuthorId,
            CategoryId = request.CategoryId
        };

        await booksRepository.AddBookAsync(bookModel, cancellationToken);
    }

    public async Task<IEnumerable<BookShortInfoView>> GetAllBooksAsync
        (PageSetting pageSettings, CancellationToken cancellationToken)
    {
        var books = await booksRepository
            .GetBooksAsync(pageSettings.PageSize, pageSettings.PageSize * (pageSettings.PageNumber - 1),
                cancellationToken);
        
        return books
            .Select(book => BookShortInfoView.MapFromModel(book,
                authorsRepository.GetAuthorByIdAsync(book.AuthorId, cancellationToken).Result,
                categoriesRepository.GetCategoryByIdAsync(book.CategoryId, cancellationToken).Result, pageSettings));
        
    }

    public async Task<BookView> GetBookByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var book = await booksRepository.GetBookByIdAsync(id, cancellationToken);
        if (book is null)
        {
            throw new NotFoundException("Book with this id not found");
        }

        return BookView.MapFromModel(book,
            await authorsRepository.GetAuthorByIdAsync(book.AuthorId, cancellationToken),
            await categoriesRepository.GetCategoryByIdAsync(book.CategoryId, cancellationToken));
    }

    public async Task<BookInfoView> GetBookInfoByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var book = await booksRepository.GetBookByIdAsync(id, cancellationToken);
        if (book is null)
        {
            throw new NotFoundException("Book with this id not found");
        }

        return BookInfoView.MapFromModel(book,
            await authorsRepository.GetAuthorByIdAsync(book.AuthorId, cancellationToken),
            await categoriesRepository.GetCategoryByIdAsync(book.CategoryId, cancellationToken));
    }

    public async Task DeleteByIdBookAsync(Guid id, CancellationToken cancellationToken)
    {
        if (!await booksRepository.BookExistsAsync(id, cancellationToken))
        {
            throw new NotFoundException("Book with this id not found");
        }
        
        await booksRepository.DeleteByIdBookAsync(id, cancellationToken);
    }

    public async Task UpdateBookInfoAsync(UpdateBookInfoRequest infoRequest, CancellationToken cancellationToken)
    {
        var book = await booksRepository.GetBookByIdAsync(infoRequest.Id, cancellationToken);
        
        if (book is null)
        {
            throw new NotFoundException("Book with this id not found");
        }
        
        if (!await authorsRepository.AuthorExistsAsync(infoRequest.AuthorId, cancellationToken))
        {
            throw new BadRequestException("Author with this id not found");
        }
        
        if (await categoriesRepository.CategoryExistsAsync(infoRequest.CategoryId, cancellationToken))
        {
            throw new BadRequestException("Category with this id not found");
        }
        
        book.Name = infoRequest.Name;
        book.Description = infoRequest.Description;
        book.AuthorId = infoRequest.AuthorId;
        book.CategoryId = infoRequest.CategoryId;
        
        await booksRepository.UpdateBookAsync(book, cancellationToken);
    }

    public async Task UpdateBookTextAsync(UpdateBookTextRequest request, CancellationToken cancellationToken)
    {
        var book = await booksRepository.GetBookByIdAsync(request.Id, cancellationToken);

        if (book is null)
        {
            throw new NotFoundException("Book with this id not found");
        }
        
        book.Text = request.Text;
        
        await booksRepository.UpdateBookAsync(book, cancellationToken);
    }

    public async Task<IEnumerable<BookShortInfoView>>
        GetAllAuthorBooksAsync(Guid authorId, PageSetting pageSettings, CancellationToken cancellationToken)
    {
        var author = await authorsRepository.GetAuthorByIdAsync(authorId, cancellationToken);
        
        if (author is null)
        {
            throw new BadRequestException("Author with this id not found");
        }
        
        var books = await authorsRepository.GetBooksByAuthorAsync(authorId, cancellationToken);
        var booksList = books.ToList() ?? throw new NotFoundException("Author doesn't have books");

        var orderedBooksTakenBooks = books.OrderBy(book => book.Name);
        
        return orderedBooksTakenBooks.Select(book =>
            BookShortInfoView.MapFromModel(book, author,
                categoriesRepository.GetCategoryByIdAsync(book.CategoryId, cancellationToken).Result, pageSettings)).ToList();
    }

    public async Task<IEnumerable<BookShortInfoView>> GetAllCategoryBooksAsync(Guid categoryId,
        PageSetting pageSettings, CancellationToken cancellationToken)
    {
        var category = await categoriesRepository.GetCategoryByIdAsync(categoryId, cancellationToken);

        if (category is null)
        {
            throw new BadRequestException("Category with this id not found");
        }
        
        var books = await booksRepository.GetBooksByCategoryAsync(categoryId,
            pageSettings.PageSize,(pageSettings.PageSize*pageSettings.PageNumber-1), cancellationToken);

        if (!books.Any())
        {
            throw new NotFoundException("No books in this category");
        }
        
        return books.Select(book =>
            BookShortInfoView.MapFromModel(book, authorsRepository.GetAuthorByIdAsync(book.AuthorId, cancellationToken).Result,
                category, pageSettings)).ToList();
    }
}