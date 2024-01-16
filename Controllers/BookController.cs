using books_api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace books_api.Controllers
{
    [Route("/api/books")]
    [ApiController]
    public class BookController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var books = new List<Book> {
                new Book() { Id =  Guid.NewGuid(), Title = "Livro 1", Author = "Author teste", Gender = "Desenvolvimento pessoal"},
                new Book() { Id =  Guid.NewGuid(), Title = "Livro 2", Author = "Author teste 2", Gender = "Ficção"},
                new Book() { Id =  Guid.NewGuid(), Title = "Livro 3", Author = "Author teste", Gender = "Desenvolvimento pessoal", Description = "Um livro pra quem quer crescer"},
                new Book() { Id =  Guid.NewGuid(), Title = "Livro 4", Author = "Author teste 3", Gender = "Fantasia", edition = "3"}
            };
            return Ok(books);
        }
    }
}
