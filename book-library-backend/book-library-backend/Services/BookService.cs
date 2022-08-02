using OneOf;
using DataAccess;
using AutoMapper;
using Domain.Dtos;
using Domain.Errors;
using Domain.Filters;
using Domain.Extensions;
using DataAccess.Models;
using Domain.Filters.Abstractions;
using Microsoft.EntityFrameworkCore;
using BookLibraryApi.Helpers;
using BookLibraryApi.Services.Contracts;

namespace BookLibraryApi.Services;

public class BookService : IBookService
{
    private readonly IMapper mapper;
    private readonly DatabaseContext dbContext;
    private readonly IHttpContextAccessor httpAccessor;

    public BookService(DatabaseContext dbContext, IMapper mapper, IHttpContextAccessor httpAccessor)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
        this.httpAccessor = httpAccessor;
    }

    public async Task<OneOf<BookView, Error>> Create(BookCreate bookModel)
    {
        if (!bookModel.ImageFile.IsValid())
        {
            return Errors.BookCreationFaild;
        }

        var book = mapper.Map<Book>(bookModel);

        using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            await dbContext.Books.AddAsync(book);

            var bookAuthors = bookModel.Authors.Select(x => new BookAuthor
            {
                AuthorId = x.Id,
                BookId = book.Id
            });
            var bookGenres = bookModel.Genres.Select(x => new BookGenre
            {
                GenreId = x.Id,
                BookId = book.Id
            });

            await dbContext.BooksGenres.AddRangeAsync(bookGenres);
            await dbContext.BooksAuthors.AddRangeAsync(bookAuthors);

            await dbContext.SaveChangesAsync();

            ImageHelper.CreateImage(bookModel.ImageFile, book.Id.ToString());

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            return Errors.BookCreationFaild;
        }

        return mapper.Map<BookView>(book);
    }



    public async Task<IEnumerable<BookView>> GetAll(Guid userId, BookFilter bookFilter)
    {
        var books = await dbContext.Books
            .SetPageFilter(bookFilter)
            .SetBookFilter(bookFilter)
            .Select(x => new BookView
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                Description = x.Description,
                PagesCount = x.PagesCount,
                Genres = x.BooksGenres.Select(x => new GenreModel
                {
                    Id = x.GenreId,
                    Name = x.Genre.Name
                }),
                Authors = x.BooksAuthors.Select(x => new AuthorModel
                {
                    Id = x.AuthorId,
                    Name = x.Author.Name
                })
            })
            .AsNoTracking()
            .ToListAsync();

        SetUrls(books);

        return books;
    }

    public async Task<IEnumerable<BookView>> GetByAuthor(Guid id, PageFilter pageFilter)
    {
        var books = await dbContext.BooksAuthors
            .SetPageFilter(pageFilter)
            .Where(x => x.AuthorId == id)
            .Select(x => new BookView
            {
                Id = x.Book.Id,
                Name = x.Book.Name,
                Image = x.Book.Image,
                Description = x.Book.Description,
                PagesCount = x.Book.PagesCount,
                Genres = x.Book.BooksGenres.Select(x => new GenreModel
                {
                    Id = x.GenreId,
                    Name = x.Genre.Name
                }),
                Authors = x.Book.BooksAuthors.Select(x => new AuthorModel
                {
                    Id = x.AuthorId,
                    Name = x.Author.Name
                })
            })
            .AsNoTracking()
            .ToListAsync();

        SetUrls(books);

        return books;
    }

    public async Task<IEnumerable<BookView>> GetByCollection(Guid collectionId, PageFilter pageFilter)
    {
        var books = await dbContext.BooksCollections
            .SetPageFilter(pageFilter)
            .Where(x => x.CollectionId == collectionId)
            .Select(x => new BookView
            {
                Id = x.Book.Id,
                Name = x.Book.Name,
                Image = x.Book.Image,
                Description = x.Book.Description,
                PagesCount = x.Book.PagesCount,
                Genres = x.Book.BooksGenres.Select(x => new GenreModel
                {
                    Id = x.GenreId,
                    Name = x.Genre.Name
                }),
                Authors = x.Book.BooksAuthors.Select(x => new AuthorModel
                {
                    Id = x.AuthorId,
                    Name = x.Author.Name
                })
            })
            .AsNoTracking()
            .ToListAsync();

        SetUrls(books);

        return books;
    }


    public async Task<OneOf<IEnumerable<BookView>, Error>> GetRecommendations(Guid userId, PageFilter pageFilter)
    {
        var reviews = await dbContext.Reviews
            .Where(x => x.UserId == userId)
            .Select(x => new
            {
                x.Rating,
                Genres = x.Book.BooksGenres.Select(x => x.GenreId)
            })
            .OrderBy(x => x.Rating)
            .AsNoTracking()
            .ToListAsync();

        if (reviews.Count < 10)
        {
            return Errors.RecommendationsFaild;
        }

        var genres = reviews
            .SelectMany(x => x.Genres)
            .Take(10)
            .ToList();

        genres.Sort();

        var distinctGenres = genres.Distinct();

        if (distinctGenres.Any())
        {
            var query = dbContext.Books.SetPageFilter(pageFilter);
            var queryGenres = dbContext.BooksGenres.Where(x => distinctGenres.Contains(x.GenreId));

            query = query.Where(x => x.BooksGenres.Intersect(queryGenres).Any());

            var books = await query
                .Select(x => new BookView
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    Description = x.Description,
                    PagesCount = x.PagesCount,
                    Genres = x.BooksGenres.Select(x => new GenreModel
                    {
                        Id = x.GenreId,
                        Name = x.Genre.Name
                    }),
                    Authors = x.BooksAuthors.Select(x => new AuthorModel
                    {
                        Id = x.AuthorId,
                        Name = x.Author.Name
                    })
                })
                .AsNoTracking()
                .ToListAsync();

            SetUrls(books);

            return books;
        }

        return Errors.RecommendationsFaild;
    }

    public async Task<OneOf<BookView, Error>> GetById(Guid bookId)
    {
        var book = await dbContext.Books
            .Select(x => new BookView
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                Description = x.Description,
                PagesCount = x.PagesCount,
                Genres = x.BooksGenres.Select(x => new GenreModel
                {
                    Id = x.GenreId,
                    Name = x.Genre.Name
                }),
                Authors = x.BooksAuthors.Select(x => new AuthorModel
                {
                    Id = x.AuthorId,
                    Name = x.Author.Name
                })
            })
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == bookId);

        if (book is null)
        {
            return Errors.BookNotExists;
        }

        SetUrl(book);

        return book;
    }

    public async Task<IEnumerable<BookView>> GetReadBooks(Guid userId)
    {
        var books = await dbContext.ReadList
            .Where(x => x.UserId == userId)
            .Select(x => new BookView
            {
                Id = x.Book.Id,
                Name = x.Book.Name,
                Image = x.Book.Image,
                Description = x.Book.Description,
                PagesCount = x.Book.PagesCount,
                Genres = x.Book.BooksGenres.Select(x => new GenreModel
                {
                    Id = x.GenreId,
                    Name = x.Genre.Name
                }),
                Authors = x.Book.BooksAuthors.Select(x => new AuthorModel
                {
                    Id = x.AuthorId,
                    Name = x.Author.Name
                })
            })
            .ToListAsync();

        SetUrls(books);

        return books;
    }

    public async Task<IEnumerable<BookView>> GetNoveltiesBooks()
    {
        var books = await dbContext.Books
            .OrderBy(x => x.Date)
            .Select(x => new BookView
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                Description = x.Description,
                PagesCount = x.PagesCount,
                Genres = x.BooksGenres.Select(x => new GenreModel
                {
                    Id = x.GenreId,
                    Name = x.Genre.Name
                }),
                Authors = x.BooksAuthors.Select(x => new AuthorModel
                {
                    Id = x.AuthorId,
                    Name = x.Author.Name
                })
            })
            .AsNoTracking()
            .ToListAsync();

        SetUrls(books);

        return books;
    }

    public async Task Remove(BookView bookModel)
    {
        var book = mapper.Map<Book>(bookModel);

        dbContext.Books.Remove(book);
        await dbContext.SaveChangesAsync();
    }

    public async Task<BookView> Update(BookView bookModel)
    {
        var book = mapper.Map<Book>(bookModel);

        dbContext.Books.Update(book);
        await dbContext.SaveChangesAsync();

        return bookModel;
    }

    public async Task<OneOf<BookRead, Error>> MarkAsRead(Guid userId, Guid bookId)
    {
        var exists = await dbContext.Books.AnyAsync(x => x.Id == bookId);

        if (!exists)
        {
            return Errors.BookNotExists;
        }

        var bookRead = new BookRead
        {
            BookId = bookId,
            UserId = userId
        };

        await dbContext.ReadList.AddAsync(bookRead);
        await dbContext.SaveChangesAsync();

        return bookRead;
    }

    private void SetUrl(BookView result)
    {
        var context = httpAccessor.HttpContext;
        if (context is not null)
        {
            result.Image = $"{context.Request.Scheme}://{context.Request.Host}/Picture/GetPicture/6a6e32c6-9f56-4306-b109-3c5b91ab5bd2";
        }
    }

    private void SetUrls(List<BookView> result)
    {
        var context = httpAccessor.HttpContext;
        if (context is not null)
        {
            for (int i = 0; i < result.Count; i++)
            {
                result[i].Image = $"{context.Request.Scheme}://{context.Request.Host}/Picture/GetPicture/6a6e32c6-9f56-4306-b109-3c5b91ab5bd2";
            }
        }
    }
}