using AutoMapper;
using BookLibraryApi.Helpers;
using Common.Dtos;
using Common.Errors;
using Common.Filters;
using Common.Filters.Abstractions;
using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace BookLibraryApi.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly DatabaseContext dbContext;
        private readonly IMapper mapper;

        public CollectionService(DatabaseContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<OneOf<CollectionModel, Error>> Create(CollectionModel model)
        {
            var collection = mapper.Map<Collection>(model);

            await dbContext.Collections.AddAsync(collection);
            await dbContext.SaveChangesAsync();

            return model;
        }

        public async Task AddBook(Guid collectionId, Guid bookId)
        {
            var bookCollection = new BookCollection
            {
                BookId = bookId,
                CollectionId = collectionId
            };

            await dbContext.BooksCollections.AddAsync(bookCollection);
            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveBook(Guid collectionId, Guid bookId)
        {
            var bookCollection = await dbContext.BooksCollections.FirstOrDefaultAsync(x => x.BookId == bookId && x.CollectionId == collectionId);

            if (bookCollection is not null)
            {
                dbContext.BooksCollections.Remove(bookCollection);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CollectionModel>> GetAll(CollectionFilter collectionFilter)
        {
            var collections = await dbContext.Collections
                .SetPageFilter(collectionFilter)
                .SetCollectionFilter(collectionFilter)
                .Select(x => new CollectionModel
                {
                    Description = x.Description,
                    Likes = x.Likes,
                    Views = x.Views
                })
                .ToListAsync();

            return collections;
        }

        public async Task<IEnumerable<CollectionModel>> GetByUser(Guid userId, PageFilter pageFilter)
        {
            return await dbContext.Collections
                .Where(x => x.UserId == userId)
                .SetPageFilter(pageFilter)
                .Select(x => new CollectionModel
                {
                    Description = x.Description,
                    Likes = x.Likes,
                    Views = x.Views
                })
                .ToListAsync();
        }

        public async Task Remove(Guid userId, Guid collectionId)
        {
            var collection = await dbContext.Collections.FirstOrDefaultAsync(x => x.UserId == userId && x.Id == collectionId);

            if (collection is not null)
            {
                dbContext.Collections.Remove(collection);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task Update(CollectionModel model)
        {
            var collection = await dbContext.Collections.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (collection is not null)
            {
                if (!string.IsNullOrEmpty(model.Name))
                {
                    collection.Name = model.Name;
                }
                if (!string.IsNullOrEmpty(model.Description))
                {
                    collection.Description = model.Description;
                }
                if (model.Views != default)
                {
                    collection.Views = model.Views;
                }
                if (model.Likes != default)
                {
                    collection.Likes = model.Likes;
                }

                dbContext.Collections.Update(collection);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
