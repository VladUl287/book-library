using OneOf;
using Domain.Dtos;
using Domain.Errors;
using Domain.Filters;
using Domain.Filters.Abstractions;
using DataAccess.Models;

namespace BookLibraryApi.Services.Contracts;

public interface IBookService
{
    Task<IEnumerable<BookView>> GetAll(Guid userId, BookFilter bookFilter);
    Task<OneOf<BookView, Error>> GetById(Guid bookId);
    Task<IEnumerable<BookView>> GetReadBooks(Guid id);
    Task<IEnumerable<BookView>> GetNoveltiesBooks();
    Task<IEnumerable<BookView>> GetByCollection(Guid id, PageFilter pageFilter);
    Task<IEnumerable<BookView>> GetByAuthor(Guid id, PageFilter pageFilter);
    Task<OneOf<IEnumerable<BookView>, Error>> GetRecommendations(Guid userId, PageFilter pageFilter);
    Task<OneOf<BookView, Error>> Create(BookCreate model);
    Task<OneOf<BookRead, Error>> MarkAsRead(Guid userId, Guid bookId);
    Task<BookView> Update(BookView model);
    Task Remove(BookView model);
}