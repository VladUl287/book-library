using DataAccess;
using Common.Dtos;
using book_library_backend.Services.Contracts;
using DataAccess.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Common.Extensions;
using Common.Errors;
using OneOf;

namespace book_library_backend.Services
{
    public class BookService : IBookService
    {
        private readonly DatabaseContext dbContext;
        private readonly IMapper mapper;

        public BookService(DatabaseContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<OneOf<BookModel, Error>> CreateBook(BookModel bookModel)
        {
            var imageValid = bookModel.Image.IsValid();

            if(!imageValid)
            {
                return Errors.LoginFaild;
            }

            var imageName = bookModel.Image.FileName;

            var book = new Book
            {
                Name = bookModel.Name,
                Description = bookModel.Description,
                Image = imageName,
                PagesCount = bookModel.PagesCount
            };

            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();

            return bookModel;
        }

        public async Task<IEnumerable<BookModel>> GetBooks()
        {
            var books = await dbContext.Books.ToListAsync();

            return mapper.Map<IEnumerable<BookModel>>(books);
        }

        public async Task RemoveBook(BookModel bookModel)
        {
            var book = mapper.Map<Book>(bookModel);

            dbContext.Books.Remove(book);
            await dbContext.SaveChangesAsync();
        }

        public async Task<BookModel> UpdateBook(BookModel bookModel)
        {
            var book = mapper.Map<Book>(bookModel);

            dbContext.Books.Update(book);
            await dbContext.SaveChangesAsync();

            return bookModel;
        }
    }
}
