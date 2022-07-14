using BookLibraryApi.Services.Contracts;
using Common.Dtos;
using Common.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BookmarksController : ControllerBase
{
    private readonly IBookmarkService bookmarkService;

    public BookmarksController(IBookmarkService bookmarkService)
    {
        this.bookmarkService = bookmarkService;
    }

    [HttpGet]
    public async Task<IEnumerable<BookModel>> Get()
    {
        var userId = User.GetLoggedInUserId<Guid>();

        return await bookmarkService.Get(userId);
    }

    [HttpPost("{bookId:Guid}")]
    public async Task<IActionResult> Add([FromRoute] Guid bookId)
    {
        var userId = User.GetLoggedInUserId<Guid>();

        await bookmarkService.Add(userId, bookId);

        return NoContent();
    }

    [HttpPost("{bookId:Guid}")]
    public async Task<IActionResult> Remove([FromRoute] Guid bookId)
    {
        var userId = User.GetLoggedInUserId<Guid>();

        await bookmarkService.Remove(userId, bookId);

        return NoContent();
    }
}