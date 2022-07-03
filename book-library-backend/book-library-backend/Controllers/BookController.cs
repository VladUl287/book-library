using Common.Dtos;
using Common.Filters;
using Microsoft.AspNetCore.Mvc;
using BookLibraryApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BookLibraryApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        var books = await bookService.GetAll(bookFilter);

        SetUrl(books.ToList());

        return Ok(books);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetPicture(Guid id)
    {
        var path = $@"{Environment.CurrentDirectory}\Files\{id}.jpg";

        return PhysicalFile(path, "image/jpeg");
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
               $"{Url.Action(nameof(GetPicture), "Book", new { id = result[i].Id })}";
        }
    }
}