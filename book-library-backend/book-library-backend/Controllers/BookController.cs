using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using book_library_backend.Services.Contracts;
using Common.Dtos;

namespace book_library_backend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BookController : ControllerBase
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            return Ok(await bookService.GetBooks());
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(BookModel bookModel)
        {
            var result = await bookService.CreateBook(bookModel);

            return result.Match<IActionResult>(
                success => NoContent(),
                error => BadRequest(error));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook(BookModel bookModel)
        {
            await bookService.UpdateBook(bookModel);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveBook(Guid id, BookModel bookModel)
        {
            await bookService.RemoveBook(bookModel);

            return NoContent();
        }
    }
}