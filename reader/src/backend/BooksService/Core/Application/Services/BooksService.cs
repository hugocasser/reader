using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Dtos.Requests;
using Application.Dtos.Requests.Books;
using Application.Dtos.Views.Books;
using Application.Exceptions;
using Domain.Models;
using MapsterMapper;

namespace Application.Services;

public class BooksService(
    IBooksRepository _booksRepository,
    IAuthorsRepository _authorsRepository,
    ICategoriesRepository _categoriesRepository,
    IMapper _mapper) : IBooksService
{
    public async Task<BookInfoViewDto> CreateBookAsync(CreateBookRequestDto requestDto, CancellationToken cancellationToken)
    {
        var author = await _authorsRepository.GetByIdAsync(requestDto.AuthorId, cancellationToken);
        
        if (author is null)
        {
            throw new BadRequestException("Author with this id not found");
        }

        var category = await _categoriesRepository.GetByIdAsync(requestDto.CategoryId, cancellationToken);
        
        if (category is null)
        {
            throw new BadRequestException("Category with this id not found");
        }

        var bookModel = new Book
        {
            Id = Guid.NewGuid(),
            Name = requestDto.Name,
            Description = requestDto.Description,
            Text = requestDto.Text,
            AuthorId = requestDto.AuthorId,
            CategoryId = requestDto.CategoryId
        };

        await _booksRepository.AddAsync(bookModel, cancellationToken);

        return _mapper.Map<BookInfoViewDto>(bookModel);
    }

    public async Task<IEnumerable<BookShortInfoViewDto>> GetAllBooksAsync
        (PageSettingRequestDto pageSettingsRequestDto, CancellationToken cancellationToken)
    {
        var books = await _booksRepository
            .GetAllAsync(pageSettingsRequestDto, cancellationToken);

        return books.Select(_mapper.Map<BookShortInfoViewDto>).ToList();
    }

    public async Task<BookViewDto> GetBookByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var book = await _booksRepository.GetByIdAsync(id, cancellationToken);
        if (book is null)
        {
            throw new NotFoundException("Book with this id not found");
        }

        return _mapper.Map<BookViewDto>(book);
    }

    public async Task<BookInfoViewDto> GetBookInfoByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var book = await _booksRepository.GetByIdAsync(id, cancellationToken);
        if (book is null)
        {
            throw new NotFoundException("Book with this id not found");
        }

        return _mapper.Map<BookInfoViewDto>(book);
    }

    public async Task DeleteByIdBookAsync(Guid id, CancellationToken cancellationToken)
    {
        if (!await _booksRepository.IsExistsAsync(id, cancellationToken))
        {
            throw new NotFoundException("Book with this id not found");
        }
        
        await _booksRepository.DeleteByIdAsync(id, cancellationToken);
    }

    public async Task<BookInfoViewDto> UpdateBookInfoAsync(UpdateBookInfoRequestDto infoRequestDto,
        CancellationToken cancellationToken)
    {
        var book = await _booksRepository.GetByIdAsync(infoRequestDto.Id, cancellationToken);
        
        if (book is null)
        {
            throw new NotFoundException("Book with this id not found");
        }
        
        var author = await _authorsRepository.GetByIdAsync(infoRequestDto.AuthorId, cancellationToken);
        
        if (author is null)
        {
            throw new BadRequestException("Author with this id not found");
        }
        
        var category = await _categoriesRepository.GetByIdAsync(infoRequestDto.CategoryId, cancellationToken);
       
        if (category is null)
        {
            throw new BadRequestException("Category with this id not found");
        }
        
        var bookToUpdate = _mapper.Map<Book>(infoRequestDto);
        
        await _booksRepository.UpdateAsync(bookToUpdate, cancellationToken);

        return _mapper.Map<BookInfoViewDto>(bookToUpdate);
    }

    public async Task<BookViewDto> UpdateBookTextAsync(UpdateBookTextRequestDto requestDto, CancellationToken cancellationToken)
    {
        if (await _booksRepository.GetByIdAsync(requestDto.Id, cancellationToken) is not Book book)
        {
            throw new NotFoundException("Book with this id not found");
        }
        
        book.Text = requestDto.Text;
        
        await _booksRepository.UpdateAsync(book, cancellationToken);

        return _mapper.Map<BookViewDto>(book);
    }

    public async Task<IEnumerable<BookShortInfoViewDto>>
        GetAllAuthorBooksAsync(Guid authorId, PageSettingRequestDto pageSettingsRequestDto, CancellationToken cancellationToken)
    {
        var author = await _authorsRepository.GetByIdAsync(authorId, cancellationToken);
        
        if (author is null)
        {
            throw new BadRequestException("Author with this id not found");
        }
        
        var books = await _authorsRepository.GetBooksByAuthorAsync(authorId, cancellationToken);
        var booksList = books.ToList();

        var orderedBooksTakenBooks = books.OrderBy(book => book.Name);
        
        return orderedBooksTakenBooks.Select(_mapper.Map<BookShortInfoViewDto>).ToList();
    }

    public async Task<IEnumerable<BookShortInfoViewDto>> GetAllCategoryBooksAsync(Guid categoryId,
        PageSettingRequestDto pageSettingsRequestDto, CancellationToken cancellationToken)
    {
        var category = await _categoriesRepository.GetByIdAsync(categoryId, cancellationToken);

        if (category is null)
        {
            throw new BadRequestException("Category with this id not found");
        }
        
        var books = await _booksRepository.GetBooksByCategoryAsync(categoryId, pageSettingsRequestDto, cancellationToken);

        if (!books.Any())
        {
            throw new NotFoundException("No books in this category");
        }

        return books.Select(_mapper.Map<BookShortInfoViewDto>).ToList();
    }
}