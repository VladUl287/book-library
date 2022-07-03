using Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using BookLibraryApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BookLibraryApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CollectionController : ControllerBase
{
    private readonly ICollectionService collectionService;

    public CollectionController(ICollectionService collectionService)
    {
        this.collectionService = collectionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await collectionService.GetAll());
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
        var book = await collectionService.Update(bookModel);

        return Ok(book);
    }

    [HttpDelete]
    public async Task<IActionResult> Remove(CollectionModel bookModel)
    {
        await collectionService.Remove(bookModel);

        return NoContent();
    }
}