using OneOf;
using Domain.Dtos;
using Domain.Errors;

namespace BookLibraryApi.Services.Contracts;

public interface IAuthorService
{
    Task<IEnumerable<AuthorModel>> GetAll();
    Task<OneOf<AuthorModel, Error>> Create(AuthorModel authorModel);
    Task<AuthorModel> Update(AuthorModel authorModel);
    Task Remove(AuthorModel authorModel);
}