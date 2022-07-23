using Domain.Dtos;
using Domain.Filters;
using Microsoft.AspNetCore.Mvc;
using BookLibraryApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Domain.Extensions;

namespace BookLibraryApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ReviewController : ControllerBase
{
    private readonly IReviewService reviewService;

    public ReviewController(IReviewService reviewService)
    {
        this.reviewService = reviewService;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetAll([FromRoute] Guid id, [FromQuery] ReviewFilter reviewFilter)
    {
        return Ok(await reviewService.Get(id, reviewFilter));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ReviewModel reviewModel)
    {
        var userId = User.GetLoggedInUserId<Guid>();

        var result = await reviewService.Create(userId, reviewModel);

        return result.Match<IActionResult>(
            success => CreatedAtAction(nameof(Create), success),
            error => BadRequest(error));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ReviewModel reviewModel)
    {
        var userId = User.GetLoggedInUserId<Guid>();

        var review = await reviewService.Update(userId, reviewModel);

        return Ok(review);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        var userId = User.GetLoggedInUserId<Guid>();

        await reviewService.Remove(userId, id);

        return NoContent();
    }
}