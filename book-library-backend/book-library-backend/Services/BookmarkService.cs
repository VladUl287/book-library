using AutoMapper;
using BookLibraryApi.Services.Contracts;
using Common.Dtos;
using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApi.Services;

public class BookmarkService : IBookmarkService
{
    private readonly DatabaseContext dbContext;
    private readonly IMapper mapper;

    public BookmarkService(DatabaseContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task Add(Guid userId, Guid bookId)
    {
        var bookmark = new Bookmark
        {
            BookId = bookId,
            UserId = userId
        };

        await dbContext.Bookmarks.AddAsync(bookmark);
        await dbContext.SaveChangesAsync();
    }

    public async Task Remove(Guid userId, Guid bookId)
    {
        var bookmark = await dbContext.Bookmarks.FirstOrDefaultAsync(x => x.UserId == userId && x.BookId == bookId);

        if (bookmark is not null)
        {
            dbContext.Bookmarks.Remove(bookmark);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<BookModel>> Get(Guid userId)
    {
        var books = await dbContext.Bookmarks
            .Where(x => x.UserId == userId)
            .Select(x => x.Book)
            .ToListAsync();

        return mapper.Map<IEnumerable<BookModel>>(books);
    }
}