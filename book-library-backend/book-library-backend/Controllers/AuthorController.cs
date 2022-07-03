using Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using BookLibraryApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BookLibraryApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService authorService;

    public AuthorController(IAuthorService authorService)
    {
        this.authorService = authorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await authorService.GetAll());
    }

    [HttpPost]
    public async Task<IActionResult> Create(AuthorModel bookModel)
    {
        var result = await authorService.Create(bookModel);

        return result.Match<IActionResult>(
            success => Created(nameof(Create), success),
            error => BadRequest(error));
    }

    [HttpPut]
    public async Task<IActionResult> Update(AuthorModel bookModel)
    {
        var book = await authorService.Update(bookModel);

        return Ok(book);
    }

    [HttpDelete]
    public async Task<IActionResult> Remove(AuthorModel bookModel)
    {
        await authorService.Remove(bookModel);

        return NoContent();
    }
}
