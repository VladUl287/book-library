using Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Common.Extensions;
using BookLibraryApi.Services;
using Common.Filters;

namespace BookLibraryApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CollectionController : ControllerBase
{
    private readonly ICollectionService collectionService;

    public CollectionController(ICollectionService collectionService)
    {
        this.collectionService = collectionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CollectionFilter collection)
    {
        return Ok(await collectionService.GetAll(collection));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CollectionModel bookModel)
    {
        var userId = User.GetLoggedInUserId<Guid>();

        var result = await collectionService.Create(userId, bookModel);

        return result.Match<IActionResult>(
            success => CreatedAtAction(nameof(Create), success),
            error => BadRequest(error));
    }

    [HttpPut]
    public async Task<IActionResult> AddBook([FromQuery] Guid collectionId, [FromQuery] Guid bookId)
    {
        await collectionService.AddBook(collectionId, bookId);

        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> RemoveBook([FromQuery] Guid collectionId, [FromQuery] Guid bookId)
    {
        await collectionService.RemoveBook(collectionId, bookId);

        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> Update(CollectionModel bookModel)
    {
        await collectionService.Update(bookModel);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Remove(Guid userId, Guid collectionId)
    {
        await collectionService.Remove(userId, collectionId);

        return NoContent();
    }
}