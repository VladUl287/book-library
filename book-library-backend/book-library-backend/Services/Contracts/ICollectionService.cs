using Common.Dtos;
using Common.Errors;
using Common.Filters;
using Common.Filters.Abstractions;
using OneOf;

namespace BookLibraryApi.Services;

public interface ICollectionService
{
    Task AddBook(Guid collectionId, Guid bookId);
    Task<OneOf<CollectionModel, Error>> Create(CollectionModel model);
    Task<IEnumerable<CollectionModel>> GetAll(CollectionFilter collectionFilter);
    Task<IEnumerable<CollectionModel>> GetByUser(Guid userId, PageFilter pageFilter);
    Task Remove(Guid userId, Guid collectionId);
    Task RemoveBook(Guid collectionId, Guid bookId);
    Task Update(CollectionModel model);
}