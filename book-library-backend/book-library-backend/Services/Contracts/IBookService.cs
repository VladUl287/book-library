using OneOf;
using Common.Dtos;
using Common.Errors;
using Common.Filters;

namespace BookLibraryApi.Services.Contracts;

public interface IBookService
{
    Task<IEnumerable<BookModel>> GetAll(BookFilter bookFilter);
    Task<OneOf<BookModel, Error>> Create(CreateBookModel model);
    Task<BookModel> Update(BookModel model);
    Task Remove(BookModel model);
}