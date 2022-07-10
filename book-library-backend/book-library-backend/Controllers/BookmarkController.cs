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
public class BookmarkController : ControllerBase
{
    private readonly IBookmarkService bookmarkService;

    public BookmarkController(IBookmarkService bookmarkService)
    {
        this.bookmarkService = bookmarkService;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IEnumerable<BookModel>> Get([FromRoute] Guid id)
    {
        return await bookmarkService.Get(id);
    }

    [HttpPost("{bookId:Guid}")]
    public async Task<IActionResult> Add([FromRoute] Guid bookId)
    {
        var userId = User.GetLoggedInUserId<Guid>();

        await bookmarkService.Add(userId, bookId);

        return Ok();
    }

    [HttpPost("{bookId:Guid}")]
    public async Task<IActionResult> Remove([FromRoute] Guid bookId)
    {
        var userId = User.GetLoggedInUserId<Guid>();

        await bookmarkService.Remove(userId, bookId);

        return Ok();
    }
}