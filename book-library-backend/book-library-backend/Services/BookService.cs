using OneOf;
using DataAccess;
using AutoMapper;
using Common.Dtos;
using Common.Errors;
using Common.Filters;
using Common.Extensions;
using DataAccess.Models;
using SixLabors.ImageSharp;
using Microsoft.EntityFrameworkCore;
using BookLibraryApi.Services.Contracts;
using SixLabors.ImageSharp.Formats.Jpeg;
using Common.Filters.Abstractions;

namespace BookLibraryApi.Services;

public class BookService : IBookService
{
    private readonly IMapper mapper;
    private readonly DatabaseContext dbContext;
    private static readonly string EnvironmentPath = @$"{Environment.CurrentDirectory}\Files\";
    public BookService(DatabaseContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<OneOf<BookModel, Error>> Create(CreateBookModel bookModel)
    {
        var imageValid = bookModel.Image.IsValid();

        if (!imageValid)
        {
            return Errors.LoginFaild;
        }

        var book = mapper.Map<Book>(bookModel);
        book.Image = book.Id.ToString();

        using var image = Image.Load(bookModel.Image.OpenReadStream());
        var path = $@"{EnvironmentPath}{book.Image}.jpg";
        image.Save(path, new JpegEncoder());

        var bookEntity = await dbContext.Books.AddAsync(book);
        await dbContext.SaveChangesAsync();

        if (bookModel.Authors.Length > 0)
        {
            var authors = mapper.Map<Author[]>(bookModel.Authors);
            var booksAuthors = new List<BookAuthor>(authors.Length);
            for (int i = 0; i < authors.Length; i++)
            {
                booksAuthors.Add(new BookAuthor
                {
                    AuthorId = authors[i].Id,
                    BookId = book.Id
                });
            }

            await dbContext.BooksAuthors.AddRangeAsync(booksAuthors);
            await dbContext.SaveChangesAsync();
        }

        return mapper.Map<BookModel>(bookEntity.Entity);
    }

    public async Task<IEnumerable<BookModel>> GetAll(Guid userId, BookFilter bookFilter)
    {
        var books = await dbContext.Books
            .SetBookFilter(bookFilter)
            .SetPageFilter(bookFilter)
            .Select(x => new BookModel
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                Description = x.Description,
                PagesCount = x.PagesCount,
                Authors = x.BooksAuthors.Select(x => new AuthorModel
                {
                    Id = x.AuthorId,
                    Name = x.Author.Name
                }).ToArray()
            })
            .AsNoTracking()
            .ToListAsync();

        var bookmarks = await dbContext.Bookmarks
            .Where(x => x.UserId == userId)
            .Select(x => x.BookId)
            .ToListAsync();

        for (int i = 0; i < books.Count; i++)
        {
            if (bookmarks.Contains(books[i].Id))
            {
                books[i].Bookmark = true;
            }
        }

        return books;
    }

    public async Task<IEnumerable<BookModel>> GetByAuthor(Guid id, PageFilter pageFilter)
    {
        var books = await dbContext.BooksAuthors
            .Where(x => x.AuthorId == id)
            .SetPageFilter(pageFilter)
            .Select(x => x.Book)
            .AsNoTracking()
            .ToListAsync();

        return mapper.Map<IEnumerable<BookModel>>(books);
    }

    public async Task<IEnumerable<BookModel>> GetByCollection(Guid collectionId, PageFilter pageFilter)
    {
        var books = await dbContext.BooksCollections
            .Where(x => x.CollectionId == collectionId)
            .SetPageFilter(pageFilter)
            .Select(x => x.Book)
            .AsNoTracking()
            .ToListAsync();

        return mapper.Map<IEnumerable<BookModel>>(books);
    }

    public async Task Remove(BookModel bookModel)
    {
        var book = mapper.Map<Book>(bookModel);

        dbContext.Books.Remove(book);
        await dbContext.SaveChangesAsync();
    }

    public async Task<BookModel> Update(BookModel bookModel)
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
            UserId = bookId
        };

        await dbContext.ReadList.AddAsync(bookRead);
        await dbContext.SaveChangesAsync();

        return bookRead;
    }

    public async Task<IEnumerable<BookModel>> GetRecommendations(Guid userId, PageFilter pageFilter)
    {
        var reviews = await dbContext.Reviews
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .Select(x => new { Rating = x.Rating, Genres = x.Book.BooksGenres.Select(x => x.GenreId) })
            .OrderBy(x => x.Rating)
            .ToListAsync();

        if (reviews.Count < 10)
        {
            return null;
        }

        var genres = reviews.Take(10).Select(x => x.Genres).ToArray();
        var genreList = new List<Guid>();
        for (int i = 0; i < 10; i++)
        {
            genreList.AddRange(genres[i]);
        }

        genreList.Sort();
        genreList.Distinct();

        if (genreList.Count > 0)
        {
            var query = dbContext.Books.AsQueryable();
            var queryGenres = dbContext.BooksGenres.Where(x => genreList.Contains(x.GenreId));

            query = query
                .SetPageFilter(pageFilter)
                .Where(x => x.BooksGenres.Intersect(queryGenres).Any());

            return mapper.Map<IEnumerable<BookModel>>(await query.ToListAsync());
        }

        return null;
    }
}