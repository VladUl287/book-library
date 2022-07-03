using OneOf;
using Common.Dtos;
using Common.Errors;

namespace BookLibraryApi.Services.Contracts;

public interface IAuthorService
{
    Task<IEnumerable<AuthorModel>> GetAll();
    Task<OneOf<AuthorModel, Error>> Create(AuthorModel authorModel);
    Task<AuthorModel> Update(AuthorModel authorModel);
    Task Remove(AuthorModel authorModel);
}