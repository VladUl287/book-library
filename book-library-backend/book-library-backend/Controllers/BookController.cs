using Domain.Dtos;
using Domain.Filters;
using Microsoft.AspNetCore.Mvc;
using BookLibraryApi.Services.Contracts;
using Domain.Filters.Abstractions;
using Domain.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BookLibraryApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BookController : ControllerBase
{
    private readonly IBookService bookService;

    public BookController(IBookService bookService)
    {
        this.bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] BookFilter bookFilter)
    {
        var userId = User.GetLoggedInUserId<Guid>();

        return Ok(await bookService.GetAll(userId, bookFilter));
    }

    [HttpGet]
    public async Task<IActionResult> GetNoveltiesBooks()
    {
        return Ok(await bookService.GetNoveltiesBooks());
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await bookService.GetById(id);

        return result.Match<IActionResult>(
            success => Ok(success),
            error => BadRequest(error));
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetByAuthor([FromRoute] Guid id, [FromQuery] PageFilter pageFilter)
    {
        return Ok(await bookService.GetByAuthor(id, pageFilter));
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetByCollection([FromRoute] Guid id, [FromQuery] PageFilter pageFilter)
    {
        return Ok(await bookService.GetByCollection(id, pageFilter));
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetRecommendations([FromQuery] PageFilter pageFilter)
    {
        var userId = User.GetLoggedInUserId<Guid>();

        var result = await bookService.GetRecommendations(userId, pageFilter);

        return result.Match<IActionResult>(
            success => Ok(success), 
            error => BadRequest(error));
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetReadBooks()
    {
        var userId = User.GetLoggedInUserId<Guid>();

        return Ok(await bookService.GetReadBooks(userId));
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Create([FromForm] BookCreate bookModel)
    {
        var result = await bookService.Create(bookModel);

        return result.Match<IActionResult>(
            success => CreatedAtAction(nameof(Create), success),
            error => BadRequest(error));
    }

    [HttpPatch("{id:Guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> MarkAsRead([FromRoute] Guid id)
    {
        var userId = User.GetLoggedInUserId<Guid>();

        var result = await bookService.MarkAsRead(userId, id);

        return result.Match<IActionResult>(
            success => NoContent(),
            error => BadRequest(error));
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Update(BookView bookModel)
    {
        var book = await bookService.Update(bookModel);

        return Ok(book);
    }

    [HttpDelete]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Remove(BookView bookModel)
    {
        await bookService.Remove(bookModel);

        return NoContent();
    }
}