using BookLibraryApi.Services.Contracts;
using Common.Dtos;
using Common.Errors;
using OneOf;

namespace BookLibraryApi.Services
{
    public class CollectionService : ICollectionService
    {
        public Task<OneOf<CollectionModel, Error>> Create(CollectionModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CollectionModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Remove(CollectionModel model)
        {
            throw new NotImplementedException();
        }

        public Task<CollectionModel> Update(CollectionModel model)
        {
            throw new NotImplementedException();
        }
    }
}
