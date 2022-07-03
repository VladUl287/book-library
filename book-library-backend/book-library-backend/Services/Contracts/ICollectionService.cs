using OneOf;
using Common.Dtos;
using Common.Errors;

namespace BookLibraryApi.Services.Contracts;

public interface ICollectionService
{
    Task<IEnumerable<CollectionModel>> GetAll();
    Task<OneOf<CollectionModel, Error>> Create(CollectionModel model);
    Task<CollectionModel> Update(CollectionModel model);
    Task Remove(CollectionModel model);
}