using bookstore_backend.DTOs;
using bookstore_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace bookstore_backend.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        [Route("/book/post-book")]
        public async Task<IActionResult> PostBook([FromBody] BookDto book)
        {
               var result = await _bookService.AddNewBook(book);   

            if(result.Success == false)
            {
                return BadRequest(result.Message);
            }

            return BadRequest(result);
        }
    }
}
