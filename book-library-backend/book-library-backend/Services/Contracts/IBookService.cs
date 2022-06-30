using Common.Dtos;
using Common.Errors;
using OneOf;

namespace book_library_backend.Services.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<BookModel>> GetBooks();

        Task<OneOf<BookModel, Error>> CreateBook(BookModel bookModel);

        Task<BookModel> UpdateBook(BookModel bookModel);

        Task RemoveBook(BookModel bookModel);
    }
}
