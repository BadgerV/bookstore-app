using bookstore_backend.DTOs;
using bookstore_backend.Services;
using bookstore_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace bookstore_backend.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }



        [HttpPost]
        [Route("/review-add-review")]
        public async Task<IActionResult> AddReview([FromBody] PostReviewDto review, [FromQuery] int bookId)
        {
            var result = await _reviewService.AddReview(bookId, review);

            if(!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("/review/get-review")]
        public async Task<IActionResult> GetreviewById([FromQuery] int id)
        {
            var result = await _reviewService.GetReviewById(id);

            if(!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("/review/get-all-reviews")]
        public async Task<IActionResult> GetAlluserReviews()
        {
            var result = await _reviewService.GetAllReviewsByUser();

            if(!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPatch]
        [Route("/review/update-review")]
        public async Task<IActionResult> UpdateReview([FromQuery] int id, [FromBody] PostReviewDto review)
        {
            var result = await _reviewService.UpdateReview(id, review);

            if(!result.Success)
            {
                return BadRequest(result.Message);
            }

            return BadRequest(result);
        }
        [HttpDelete]
        [Route("/review/delete-review")]
        public async Task<IActionResult> DeleteReview([FromQuery] int id)
        {
            var result = await _reviewService.DeleteReview(id);

            if(!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}
