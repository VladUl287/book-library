using OneOf;
using AutoMapper;
using DataAccess;
using Common.Dtos;          
using Common.Errors;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using BookLibraryApi.Services.Contracts;
using Common.Filters;

namespace BookLibraryApi.Services;

public class ReviewService : IReviewService
{
    private readonly IMapper mapper;
    private readonly DatabaseContext dbContext;

    public ReviewService(DatabaseContext dbContext, IMapper mapper)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }

    public async Task<OneOf<ReviewModel, Error>> Create(ReviewModel reviewModel)
    {
        var review = mapper.Map<Review>(reviewModel);

        await dbContext.Reviews.AddAsync(review);
        await dbContext.SaveChangesAsync();

        return reviewModel;
    }

    public async Task<IEnumerable<ReviewModel>> GetAll(ReviewFilter reviewModel)
    {
        var reviews = await dbContext.Reviews.ToListAsync();

        return mapper.Map<IEnumerable<ReviewModel>>(reviews);
    }

    public async Task Remove(ReviewModel reviewModel)
    {
        var review = mapper.Map<Review>(reviewModel);

        dbContext.Reviews.Remove(review);
        await dbContext.SaveChangesAsync();
    }

    public async Task<ReviewModel> Update(ReviewModel reviewModel)
    {
        var review = mapper.Map<Review>(reviewModel);

        dbContext.Reviews.Update(review);
        await dbContext.SaveChangesAsync();

        return reviewModel;
    }
}