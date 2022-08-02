using Domain.Dtos;
using Domain.Errors;
using Domain.Filters;
using Domain.Filters.Abstractions;
using OneOf;

namespace BookLibraryApi.Services.Contracts;

public interface ICollectionService
{
    Task<IEnumerable<CollectionView>> GetAll(CollectionFilter collectionFilter);
    Task<IEnumerable<CollectionView>> GetByUser(Guid userId, PageFilter pageFilter);
    Task<OneOf<CollectionView, Error>> Create(Guid userId, CollectionCreate model);
    Task AddBook(Guid collectionId, Guid bookId);
    Task RemoveBook(Guid collectionId, Guid bookId);
    Task Remove(Guid userId, Guid collectionId);
    Task Update(CollectionView model);
    Task Like(Guid userId, Guid collectionId);
}