using Common.Dtos;
using Common.Filters;
using Microsoft.AspNetCore.Mvc;
using BookLibraryApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Common.Extensions;

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

    [HttpGet("{bookId:Guid}")]
    public async Task<IActionResult> GetAll(Guid bookId, [FromQuery] ReviewFilter reviewFilter)
    {
        return Ok(await reviewService.Get(bookId, reviewFilter));
    }

    [HttpPost]
    public async Task<IActionResult> Create(ReviewModel reviewModel)
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
        var review = await reviewService.Update(reviewModel);

        return Ok(review);
    }

    [HttpDelete]
    public async Task<IActionResult> Remove(ReviewModel reviewModel)
    {
        await reviewService.Remove(reviewModel);

        return NoContent();
    }
}