using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace books_api.Controllers
{
    [Route("/api/books")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly BooksService _booksService;

        public BookController(BooksService booksService) =>
            _booksService = booksService;

        [HttpGet]
        public async Task<List<Book>> Get() =>
        await _booksService.GetAsync();

        [HttpPost]
        public async Task<IActionResult> Post(Book newBook)
        {
            await _booksService.CreateAsync(newBook);

            return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
        }
    }
}
