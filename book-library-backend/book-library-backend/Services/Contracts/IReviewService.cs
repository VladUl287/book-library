using OneOf;
using Domain.Dtos;
using Domain.Errors;
using Domain.Filters;

namespace BookLibraryApi.Services.Contracts;

public interface IReviewService
{
    Task<IEnumerable<ReviewModel>> Get(Guid bookId, ReviewFilter reviewFilter);
    Task<OneOf<ReviewModel, Error>> Create(Guid userId, ReviewModel reviewModel);
    Task<OneOf<ReviewModel, Error>> Update(Guid userId, ReviewModel reviewModel);
    Task Remove(Guid userId, Guid reviewId);
}