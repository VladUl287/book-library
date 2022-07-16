using OneOf;
using AutoMapper;
using DataAccess;
using Common.Dtos;
using Common.Errors;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using BookLibraryApi.Services.Contracts;
using Common.Filters;
using Common.Extensions;

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

    public async Task<IEnumerable<ReviewModel>> Get(Guid bookId, ReviewFilter reviewFilter)
    {
        var reviews = await dbContext.Reviews
            .SetPageFilter(reviewFilter)
            .SetReviewFilter(reviewFilter)
            .Where(x => x.BookId == bookId)
            .Select(x => new ReviewModel
            {
                Id = x.Id,
                Text = x.Text,
                Rating = x.Rating,
                BookId = x.BookId
            })
            .AsNoTracking()
            .ToListAsync();

        return reviews;
    }

    public async Task<OneOf<ReviewModel, Error>> Create(Guid userId, ReviewModel reviewModel)
    {
        var review = mapper.Map<Review>(reviewModel);
        review.UserId = userId;
        review.DateCreate = DateTime.UtcNow;

        await dbContext.Reviews.AddAsync(review);

        var book = await dbContext.Books
            .FirstOrDefaultAsync(e => e.Id == reviewModel.BookId);

        if (book is not null)
        {
            var reviews = await dbContext.Reviews
                .AsNoTracking()
                .Where(x => x.BookId == reviewModel.BookId)
                .Select(x => x.Rating)
                .ToListAsync();

            book.Rating = reviews.Sum() / reviews.Count;
            await dbContext.SaveChangesAsync();

            var bookRead = new BookRead
            {
                BookId = book.Id,
                UserId = userId
            };

            await dbContext.ReadList.AddAsync(bookRead);
            await dbContext.SaveChangesAsync();
        }

        return reviewModel;
    }

    public async Task Remove(Guid userId, Guid reviewId)
    {
        var review = await dbContext.Reviews
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == reviewId);

        if (review is null)
        {
            return;
        }

        dbContext.Reviews.Remove(review);
        await dbContext.SaveChangesAsync();
    }

    public async Task<ReviewModel> Update(Guid userId, ReviewModel reviewModel)
    {
        var review = await dbContext.Reviews
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == userId && x.BookId == reviewModel.BookId);

        if (review is null)
        {
            return null;
        }

        var reviewUpdate = mapper.Map<Review>(reviewModel);

        dbContext.Reviews.Update(reviewUpdate);
        await dbContext.SaveChangesAsync();

        return reviewModel;
    }
}