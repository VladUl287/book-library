using OneOf;
using Common.Dtos;
using Common.Errors;
using Common.Filters;

namespace BookLibraryApi.Services.Contracts;

public interface IReviewService
{
    Task<IEnumerable<ReviewModel>> Get(Guid id, ReviewFilter reviewFilter);
    Task<OneOf<ReviewModel, Error>> Create(ReviewModel reviewModel);
    Task<ReviewModel> Update(ReviewModel reviewModel);
    Task Remove(ReviewModel reviewModel);
}