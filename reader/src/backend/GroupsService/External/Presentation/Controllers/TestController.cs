using Application.Abstractions.Repositories;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controllers;

public class TestController : ApiController
{
    private readonly IBooksRepository _booksRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IGroupsRepository _groupsRepository;

    public TestController(ISender sender,
        IBooksRepository booksRepository,
        IUsersRepository usersRepository,
        IGroupsRepository groupsRepository) : base(sender)
    {
        _booksRepository = booksRepository;
        _usersRepository = usersRepository;
        _groupsRepository = groupsRepository;
    }

    [HttpPost]
    [Route("user")]

public async Task<IActionResult> CreateUserAsync()
    {
        var random = new Random();
        var user = new User()
        {
            Id = Guid.NewGuid(),
        };
        
        user.CreateUser("test" + random.Next(0,100).ToString(), "test" + random.Next(0, 100).ToString());
        
        await _usersRepository.CreateAsync(user, default);

        await _usersRepository.SaveChangesAsync(default);
        
        return Ok(user);
    }

    [HttpPost]
    [Route("group")]
    public async Task<IActionResult> CreateGroupAsync([FromBody]Guid userId)
    {
        
        var random = new Random();
        var group = new Group()
        {
            Id = Guid.NewGuid(),
        };

        var user = await _usersRepository.GetByIdAsync(userId, default);
        
        group.CreateGroup(user, "test" + random.Next(0, 100).ToString());
        
        await _groupsRepository.CreateGroupAsync(group, default);
        
        await _groupsRepository.SaveChangesAsync(default);
        
        return Ok(group);
    }

    [HttpPost]
    [Route("book")]
    public async Task<IActionResult> CreateBookAsync()
    {
        var random = new Random();
        var book = new Book();
        
        book.CreateBook("test" + random.Next(0, 100).ToString(),
            "test" + random.Next(0, 100).ToString(),
            "test" + random.Next(0, 100).ToString(),
            Guid.NewGuid());
        
        await _booksRepository.CreateAsync(book, default);
        
        await _booksRepository.SaveChangesAsync(default);
        
        return Ok(book);
    }
}