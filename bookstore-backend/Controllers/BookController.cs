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
        public async Task<IActionResult> PostBook([FromBody] CreateBookDto book)
        {
               var result = await _bookService.AddNewBook(book);   

            if(result.Success == false)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("/book/get-book")]
        public async Task<IActionResult> GetBook([FromQuery] int id)
        {
            var result =await _bookService.GetBook(id);

            if(!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("/book/get-books")]
        public async Task<IActionResult> GetBooks([FromQuery] int page, int pageSize)
        {
            var result = await _bookService.GetBooks(page, pageSize);

            if(!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
        [HttpGet]
        [Route("/book/get-books-by-category")]
        public async Task<IActionResult> GetBooksByCategory([FromQuery] string category, int page = 1, int pageSize= 10)
        {
            var result = await _bookService.GetBooksByCategory(category, page, pageSize);

            if(!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
        [HttpDelete]
        [Route("/book/delete-book")]
        public async Task<IActionResult> DeleteBook([FromQuery] int id)
        {
            var result = await _bookService.DeleteBook(id);

            if(!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
        [HttpPatch]
        [Route("/book/update-book")]
        public async Task<IActionResult> UpdateBook([FromQuery] int id, [FromBody] CreateBookDto book)
        {
            var result = await _bookService.UpdateBook(book, id);

            if(!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
        [HttpGet]
        [Route("/book/books-by-the-same-author")]
        public async Task<IActionResult> GetBooksByTheSameAuthor([FromQuery] string authorName)
        {
            var result = await _bookService.GetBooksByTheSameAuthor(authorName);

            if(!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("/book/recommend-books")]
        public async Task<IActionResult> GetBooksByRecommendation()
        {
            var result = await _bookService.GetBooksByRecommendation();

            if(!result.Success)
            {
                return BadRequest(result?.Message);
            }

            return Ok(result);
        }
        [HttpGet]
        [Route("/book/books-by-search-query")]
        public async Task<IActionResult> GetBooksBySearchQuery([FromQuery] string query)
        {
            var result = await _bookService.GetBooksBySearchQuery(query);

            if(!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("/book/book-review")]
        public async Task<IActionResult> GetReviewForBook([FromQuery] int id)
        {
            var result = await _bookService.GetReviewsForBook(id);

            if(!result.Success)
            {
                return BadRequest(result?.Message);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("/book/get-average-rating")]
        public async Task<IActionResult> GetAverageBookRating([FromQuery] int id)
        {
            var result = await _bookService.GetAverageRatingForBook(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);

        }
    }
}
