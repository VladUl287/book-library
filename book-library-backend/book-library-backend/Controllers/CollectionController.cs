using BookLibraryApi.Services.Contracts;
using Domain.Dtos;
using Domain.Extensions;
using Domain.Filters;
using Domain.Filters.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> GetAll([FromQuery] CollectionFilter collection)
    {
        return Ok(await collectionService.GetAll(collection));
    }

    [HttpGet]
    public async Task<IActionResult> GetUserCollections([FromQuery] PageFilter pageFilter)
    {
        var userId = User.GetLoggedInUserId<Guid>();

        return Ok(await collectionService.GetByUser(userId, pageFilter));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CollectionCreate bookModel)
    {
        var userId = User.GetLoggedInUserId<Guid>();

        var result = await collectionService.Create(userId, bookModel);

        return result.Match<IActionResult>(
            success => CreatedAtAction(nameof(Create), success),
            error => BadRequest(error));
    }

    [HttpPut]
    public async Task<IActionResult> AddBook([FromForm] CollectionBook manageBook)
    {
        await collectionService.AddBook(manageBook.CollectionId, manageBook.BookId);

        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> RemoveBook([FromForm] CollectionBook manageBook)
    {
        await collectionService.RemoveBook(manageBook.CollectionId, manageBook.BookId);

        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> Update(CollectionView bookModel)
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


    [HttpPut]
    public async Task<IActionResult> Like(Guid collectionId)
    {
        var userId = User.GetLoggedInUserId<Guid>();

        await collectionService.Like(userId, collectionId);

        return NoContent();
    }
}