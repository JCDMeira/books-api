using BookStoreApi.Models;
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
                new Book() {  Title = "Livro 1", Author = "Author teste", Category = "Desenvolvimento pessoal"},
                new Book() { Title = "Livro 2", Author = "Author teste 2", Category = "Ficção"},
                new Book() { Title = "Livro 3", Author = "Author teste", Category = "Desenvolvimento pessoal", Description = "Um livro pra quem quer crescer"},
                new Book() { Title = "Livro 4", Author = "Author teste 3", Category = "Fantasia", edition = "3"}
            };
            return Ok(books);
        }
    }
}
