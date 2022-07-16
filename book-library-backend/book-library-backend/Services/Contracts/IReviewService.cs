using OneOf;
using Common.Dtos;
using Common.Errors;
using Common.Filters;

namespace BookLibraryApi.Services.Contracts;

public interface IReviewService
{
    Task<IEnumerable<ReviewModel>> Get(Guid bookId, ReviewFilter reviewFilter);
    Task<OneOf<ReviewModel, Error>> Create(Guid userId, ReviewModel reviewModel);
    Task<ReviewModel> Update(Guid userId, ReviewModel reviewModel);
    Task Remove(Guid userId, Guid reviewId);
}