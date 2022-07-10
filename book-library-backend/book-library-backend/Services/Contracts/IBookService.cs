using OneOf;
using Common.Dtos;
using Common.Errors;
using Common.Filters;
using Common.Filters.Abstractions;

namespace BookLibraryApi.Services.Contracts;

public interface IBookService
{
    Task<IEnumerable<BookModel>> GetAll(Guid userId, BookFilter bookFilter);
    Task<IEnumerable<BookModel>> GetByCollection(Guid id, PageFilter pageFilter);
    Task<OneOf<BookModel, Error>> Create(CreateBookModel model);
    Task<BookModel> Update(BookModel model);
    Task Remove(BookModel model);
}