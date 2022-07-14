using Common.Dtos;
using Common.Errors;
using Common.Filters;
using Common.Filters.Abstractions;
using OneOf;

namespace BookLibraryApi.Services;

public interface ICollectionService
{
    Task<IEnumerable<CollectionModel>> GetAll(CollectionFilter collectionFilter);
    Task<IEnumerable<CollectionModel>> GetByUser(Guid userId, PageFilter pageFilter);
    Task<OneOf<CollectionModel, Error>> Create(Guid userId, CreateCollection model);
    Task AddBook(Guid collectionId, Guid bookId);
    Task RemoveBook(Guid collectionId, Guid bookId);
    Task Remove(Guid userId, Guid collectionId);
    Task Update(CollectionModel model);
}