using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Dtos.Requests;
using Application.Dtos.Requests.Authors;
using Application.Dtos.Views.Authors;
using Application.Exceptions;
using Domain.Abstractions.Events;
using Domain.Events;
using Domain.Models;
using MapsterMapper;

namespace Application.Services;

public class AuthorsService
    (IAuthorsRepository _authorsRepository, 
        IBooksRepository _booksRepository,
        IMapper _mapper,
        IKafkaProducerService<Book> _producer): IAuthorsService
{
    public async Task<AuthorViewDto> CreateAuthorAsync(CreateAuthorRequestDto requestDto, CancellationToken cancellationToken)
    {
        var author = _mapper.Map<Author>(requestDto);
        author.Id = Guid.NewGuid();
        
        await _authorsRepository.AddAsync(author, cancellationToken);

        return _mapper.Map<AuthorViewDto>(author);
    }

    public async Task<IEnumerable<AuthorShortViewDto>> GetAllAuthorsAsync(PageSettingRequestDto pageSettingsRequestDto,
        CancellationToken cancellationToken)
    {
        var authors = await _authorsRepository
            .GetAllAsync(pageSettingsRequestDto, cancellationToken);

        return authors.Select(_mapper.Map<AuthorShortViewDto>).ToList();
    }

    public async Task<AuthorViewDto> GetAuthorByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var author = await _authorsRepository.GetByIdAsync(id, cancellationToken);
        
        if (author is null)
        {
            throw new NotFoundException("Author with this id not found");
        }
        
        return _mapper.Map<AuthorViewDto>(author);
    }

    public async Task DeleteByIdAuthorAsync(Guid id, CancellationToken cancellationToken)
    { 
        if (!await _authorsRepository.IsExistsAsync(id, cancellationToken))
        {
            throw new NotFoundException("Author with this id not found");
        }

        var authorBooks = await _authorsRepository.GetBooksByAuthorAsync(id, cancellationToken); 
        
        if (authorBooks.Any())
        {
            throw new BadRequestException("Can't delete author with books");
        }
        
        await _authorsRepository.DeleteByIdAsync(id, cancellationToken);
    }

    public async Task<AuthorViewDto> UpdateAuthorAsync(UpdateAuthorRequestDto requestDto, CancellationToken cancellationToken)
    {
        var author = await _authorsRepository.GetByIdAsync(requestDto.Id, cancellationToken) as Author;

        if (author is null)
        {
            throw new NotFoundException("Author with this id not found");
        }

        var authorToUpdate = _mapper.Map<Author>(requestDto);
        
        await _authorsRepository.UpdateAsync(authorToUpdate, cancellationToken);

        var authorBooks = await _authorsRepository.GetBooksByAuthorAsync(requestDto.Id, cancellationToken);

        foreach (var book in authorBooks)
        {
            await _booksRepository.UpdateAsync(book, cancellationToken);
            await _producer.SendEventAsync(new GenericDomainEvent<Book>(book, EventType.Updated));
        }
        
        return _mapper.Map<AuthorViewDto>(authorToUpdate);
    }
}