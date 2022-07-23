using BookLibraryApi.Services.Contracts;
using Domain.Dtos;
using Domain.Extensions;
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
    public async Task<IEnumerable<BookView>> Get()
    {
        var userId = User.GetLoggedInUserId<Guid>();

        return await bookmarkService.Get(userId);
    }

    [HttpPost("{id:Guid}")]
    public async Task<IActionResult> Add([FromRoute] Guid id)
    {
        var userId = User.GetLoggedInUserId<Guid>();

        await bookmarkService.Add(userId, id);

        return NoContent();
    }

    [HttpPost("{id:Guid}")]
    public async Task<IActionResult> Remove([FromRoute] Guid id)
    {
        var userId = User.GetLoggedInUserId<Guid>();

        await bookmarkService.Remove(userId, id);

        return NoContent();
    }
}