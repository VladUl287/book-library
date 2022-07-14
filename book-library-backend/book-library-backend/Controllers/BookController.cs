using Common.Dtos;
using Common.Filters;
using Microsoft.AspNetCore.Mvc;
using BookLibraryApi.Services.Contracts;
using Common.Filters.Abstractions;
using Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BookLibraryApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        var books = await bookService.GetAll(userId, bookFilter);

        SetUrl(books.ToList());

        return Ok(books);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetByAuthor([FromRoute] Guid id, [FromQuery] PageFilter pageFilter)
    {
        var books = await bookService.GetByAuthor(id, pageFilter);

        SetUrl(books.ToList());

        return Ok(books);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetByCollection([FromRoute] Guid id, [FromQuery] PageFilter pageFilter)
    {
        var books = await bookService.GetByCollection(id, pageFilter);

        SetUrl(books.ToList());

        return Ok(books);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateBookModel bookModel)
    {
        var result = await bookService.Create(bookModel);
        var action = Url.Action(nameof(Create));

        return result.Match<IActionResult>(
            success => Created(action, success),
            error => BadRequest(error));
    }

    [HttpPut]
    public async Task<IActionResult> Update(BookModel bookModel)
    {
        var book = await bookService.Update(bookModel);

        return Ok(book);
    }

    [HttpDelete]
    public async Task<IActionResult> Remove(BookModel bookModel)
    {
        await bookService.Remove(bookModel);

        return NoContent();
    }

    private void SetUrl(List<BookModel> result)
    {
        for (int i = 0; i < result.Count; i++)
        {
            result[i].Image = $"{Request.Scheme}://{Request.Host}" +
               //$"{Url.Action("GetPicture", "Picture", new { id = result[i].Id })}";
               $"{Url.Action("GetPicture", "Picture", new { id = Guid.Parse("6a6e32c6-9f56-4306-b109-3c5b91ab5bd2") })}";
        }
    }
}