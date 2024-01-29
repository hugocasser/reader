using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Common;
using Application.Dtos;
using Application.Dtos.Requests;
using Application.Dtos.Requests.Books;
using Application.Dtos.Views;
using Application.Dtos.Views.Books;
using Application.Exceptions;
using Domain.Models;

namespace Application.Services;

public class BooksService(
    IBooksRepository booksRepository,
    IAuthorRepository authorRepository,
    ICategoryRepository categoryRepository) : IBooksService
{
    public async Task CreateBookAsync(CreateBookRequest request)
    {
        if (!await authorRepository.AuthorExistsAsync(request.AuthorId))
        {
            throw new BadRequestExceptionWithStatusCode("Author with this id not found");
        }

        if (await categoryRepository.CategoryExistsAsync(request.CategoryId))
        {
            throw new BadRequestExceptionWithStatusCode("Category with this id not found");
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

        await booksRepository.AddBookAsync(bookModel);
    }

    public async Task<IEnumerable<BookShortInfoView>>
        GetAllBooksAsync(PageSettings pageSettings)
    {
        var books = await booksRepository.GetBooksAsync();
        var orderedBooksTakenBooks = books.OrderBy(book => book.Name)
            .Skip(pageSettings.PageSize * pageSettings.PageNumber - 1).Take(pageSettings.PageSize);
        var viewBooks = orderedBooksTakenBooks
            .Select(book => BookShortInfoView.MapFromModel(book,
                authorRepository.GetAuthorByIdAsync(book.AuthorId).Result,
                categoryRepository.GetCategoryByIdAsync(book.CategoryId).Result, pageSettings)).ToList();

        return viewBooks;
    }

    public async Task<BookView> GetBookByIdAsync(Guid id)
    {
        var book = await booksRepository.GetBookByIdAsync(id);
        if (book is null)
        {
            throw new NotFoundExceptionWithStatusCode("Book with this id not found");
        }

        return BookView.MapFromModel(book,
            await authorRepository.GetAuthorByIdAsync(book.AuthorId),
            await categoryRepository.GetCategoryByIdAsync(book.CategoryId));
    }

    public async Task<BookInfoView> GetBookInfoByIdAsync(Guid id)
    {
        var book = await booksRepository.GetBookByIdAsync(id);
        if (book is null)
        {
            throw new NotFoundExceptionWithStatusCode("Book with this id not found");
        }

        return BookInfoView.MapFromModel(book,
            await authorRepository.GetAuthorByIdAsync(book.AuthorId),
            await categoryRepository.GetCategoryByIdAsync(book.CategoryId));
    }

    public async Task DeleteByIdBookAsync(Guid id)
    {
        if (!await booksRepository.BookExistsAsync(id))
        {
            throw new NotFoundExceptionWithStatusCode("Book with this id not found");
        }
        
        await booksRepository.DeleteByIdBookAsync(id);
    }

    public async Task UpdateBookInfoAsync(UpdateBookRequest request)
    {
        var book = await booksRepository.GetBookByIdAsync(request.Id);
        
        if (book is null)
        {
            throw new NotFoundExceptionWithStatusCode("Book with this id not found");
        }
        
        if (!await authorRepository.AuthorExistsAsync(request.AuthorId))
        {
            throw new BadRequestExceptionWithStatusCode("Author with this id not found");
        }
        
        if (await categoryRepository.CategoryExistsAsync(request.CategoryId))
        {
            throw new BadRequestExceptionWithStatusCode("Category with this id not found");
        }
        
        book.Name = request.Name;
        book.Description = request.Description;
        book.AuthorId = request.AuthorId;
        book.CategoryId = request.CategoryId;
        
        await booksRepository.UpdateBookAsync(book);
    }

    public async Task UpdateBookTextAsync(UpdateBookTextRequest request)
    {
        var book = await booksRepository.GetBookByIdAsync(request.Id);

        if (book is null)
        {
            throw new NotFoundExceptionWithStatusCode("Book with this id not found");
        }
        
        book.Text = request.Text;
        
        await booksRepository.UpdateBookAsync(book);
    }

    public async Task<IEnumerable<BookShortInfoView>>
        GetAllAuthorBooksAsync(Guid authorId, PageSettings pageSettings)
    {
        var author = await authorRepository.GetAuthorByIdAsync(authorId);
        
        if (author is null)
        {
            throw new BadRequestExceptionWithStatusCode("Author with this id not found");
        }
        
        var books = await booksRepository.GetBooksByAuthorAsync(authorId);
        
        if (!books.Any())
        {
            throw new NotFoundExceptionWithStatusCode("Author doesn't have books");
        }

        var orderedBooksTakenBooks = books.OrderBy(book => book.Name);
        
        return orderedBooksTakenBooks.Select(book =>
            BookShortInfoView.MapFromModel(book, author,
                categoryRepository.GetCategoryByIdAsync(book.CategoryId).Result, pageSettings)).ToList();
    }

    public async Task<IEnumerable<BookShortInfoView>>
        GetAllCategoryBooksAsync(Guid categoryId, PageSettings pageSettings)
    {
        var category = await categoryRepository.GetCategoryByIdAsync(categoryId);

        if (category is null)
        {
            throw new BadRequestExceptionWithStatusCode("Category with this id not found");
        }
        
        var books = await booksRepository.GetBooksByCategoryAsync(categoryId);

        if (!books.Any())
        {
            throw new NotFoundExceptionWithStatusCode("No books in this category");
        }
        
        var orderedBooksTakenBooks = books.OrderBy(book => book.Name);
        
        return orderedBooksTakenBooks.Select(book =>
            BookShortInfoView.MapFromModel(book, authorRepository.GetAuthorByIdAsync(book.AuthorId).Result,
                category, pageSettings)).ToList();
    }
}