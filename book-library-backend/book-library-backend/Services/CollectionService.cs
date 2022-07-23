using OneOf;
using DataAccess;
using AutoMapper;
using Domain.Dtos;
using Domain.Errors;
using Domain.Filters;
using DataAccess.Models;
using Domain.Extensions;
using Domain.Filters.Abstractions;
using Microsoft.EntityFrameworkCore;
using BookLibraryApi.Services.Contracts;

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

        public async Task<IEnumerable<CollectionView>> GetAll(CollectionFilter collectionFilter)
        {
            var collections = await dbContext.Collections
                .SetPageFilter(collectionFilter)
                .SetCollectionFilter(collectionFilter)
                .OrderBy(x => x.DateCreate)
                .Select(x => new CollectionView
                {
                    Name = x.Name,
                    Description = x.Description,
                    Likes = x.Likes,
                    Views = x.Views
                })
                .AsNoTracking()
                .ToListAsync();

            return collections;
        }

        public async Task<IEnumerable<CollectionView>> GetByUser(Guid userId, PageFilter pageFilter)
        {
            return await dbContext.Collections
                .SetPageFilter(pageFilter)
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.DateCreate)
                .Select(x => new CollectionView
                {
                    Name = x.Name,
                    Description = x.Description,
                    Likes = x.Likes,
                    Views = x.Views
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<OneOf<CollectionView, Error>> Create(Guid userId, CollectionCreate model)
        {
            if (await dbContext.Collections.AnyAsync(x => x.UserId == userId && x.Name == model.Name))
            {
                return Errors.CollectionAlreadyExists;
            }

            var collection = new Collection
            {
                Name = model.Name,
                Description = model.Name,
                UserId = userId
            };

            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                await dbContext.Collections.AddAsync(collection);

                var booksCollections = new BookCollection[model.Books.Length];
                for (int i = 0; i < model.Books.Length; i++)
                {
                    booksCollections[i] = new BookCollection
                    {
                        BookId = model.Books[i].Id,
                        CollectionId = collection.Id
                    };
                }

                await dbContext.BooksCollections.AddRangeAsync(booksCollections);

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                return Errors.CollectionCreationFaild;
            }

            return mapper.Map<CollectionView>(collection);
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
            var bookCollection = await dbContext.BooksCollections
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.BookId == bookId && x.CollectionId == collectionId);

            if (bookCollection is not null)
            {
                dbContext.BooksCollections.Remove(bookCollection);
                await dbContext.SaveChangesAsync();
            }
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

        public async Task Update(CollectionView model)
        {
            var collection = mapper.Map<Collection>(model);

            dbContext.Collections.Update(collection);
            await dbContext.SaveChangesAsync();
        }
    }
}
