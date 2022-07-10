using Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Common.Extensions;
using BookLibraryApi.Services;
using Common.Filters.Abstractions;

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
    public async Task<IActionResult> GetAll(PageFilter pageFilter)
    {
        var userId = User.GetLoggedInUserId<Guid>();

        var collections = await collectionService.GetByUser(userId, pageFilter);

        return Ok(collections);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CollectionModel bookModel)
    {
        var result = await collectionService.Create(bookModel);

        return result.Match<IActionResult>(
            success => Created(nameof(Create), success),
            error => BadRequest(error));
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