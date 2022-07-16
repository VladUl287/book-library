using OneOf;
using Common.Dtos;
using Common.Errors;
using Common.Filters;
using Common.Filters.Abstractions;
using DataAccess.Models;

namespace BookLibraryApi.Services.Contracts;

public interface IBookService
{
    Task<IEnumerable<BookModel>> GetAll(Guid userId, BookFilter bookFilter);
    Task<IEnumerable<BookModel>> GetByCollection(Guid id, PageFilter pageFilter);
    Task<IEnumerable<BookModel>> GetByAuthor(Guid id, PageFilter pageFilter);
    Task<IEnumerable<BookModel>> GetRecommendations(Guid userId, PageFilter pageFilter);
    Task<OneOf<BookModel, Error>> Create(CreateBookModel model);
    Task<OneOf<BookRead, Error>> MarkAsRead(Guid userId, Guid bookId);
    Task<BookModel> Update(BookModel model);
    Task Remove(BookModel model);
}