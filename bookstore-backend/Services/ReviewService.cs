using bookstore_backend.Data;
using bookstore_backend.DTOs;
using bookstore_backend.models;
using bookstore_backend.Services.Interfaces;
using bookstore_backend.Utilities;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace bookstore_backend.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUserService _userService;
        private readonly BookStoreDbContext _dbContext;
        private readonly IBookService _bookService;

        public ReviewService(IUserService userService, BookStoreDbContext bookStoreDbContext, IBookService bookService)
        {
            _userService = userService;
            _dbContext = bookStoreDbContext;
            _bookService = bookService;
        }
        public async Task<ApiResponse<bool>> AddReview(int bookId, PostReviewDto review)
        {
            var resultObject = await _userService.GetCurrentUser();

            if (!resultObject.Success || resultObject.User!.Username == null)
            {
                return new ApiResponse<bool>(false, "Please authenticate");
            }

            if (string.IsNullOrEmpty(review.Comment))
            {
                return new ApiResponse<bool>(false, "Please provide a comment");
            }

            // Validate if the rating is a valid integer and within a valid range if necessary
            if (review.Rating < 1 || review.Rating > 5)
            {
                return new ApiResponse<bool>(false, "Please provide a valid rating between 1 and 5");
            }

            var resultOfReviewOfBooks = await _bookService.GetReviewsForBook(bookId);
            Console.WriteLine(resultOfReviewOfBooks);

            if (resultOfReviewOfBooks.Data.Count() > 0)
            {
                var hadReviewed = resultOfReviewOfBooks.Data.Any(x => x.UserUsername == resultObject.User.Username);

                if (hadReviewed)
                {
                    return new ApiResponse<bool>(false, "You already reviewed this book");
                }
            }


            var newReview = new Review
            {
                BookId = bookId,
                Comment = review.Comment,
                Rating = review.Rating,
                UserId = resultObject.User.Id
            };

            try
            {
                _dbContext.Reviews.Add(newReview);
                await _dbContext.SaveChangesAsync();
                return new ApiResponse<bool>(true, "Success", true);
            }
            catch (DbUpdateException ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"Error adding review: {ex.Message}");
                return new ApiResponse<bool>(false, "Database error occurred");
            }
        }


        public async Task<ApiResponse<bool>> DeleteReview(int id)
        {
            var resultObject = await _userService.GetCurrentUser();

            if(!resultObject.Success || resultObject.User!.Username == null)
            {
                return new ApiResponse<bool>(false, "Please authenticate");
            }

            var particularReview = await _dbContext.Reviews.FirstOrDefaultAsync(x => x.Id == id);

            if(particularReview == null)
            {
                return new ApiResponse<bool>(false, "Review not found");
            }

            if(particularReview.UserId != resultObject.User.Id)
            {
                return new ApiResponse<bool>(false, "Unauthorized, unable to delete review");
            }

            _dbContext.Reviews.Remove(particularReview);

            try
            {
                await _dbContext.SaveChangesAsync();
                return new ApiResponse<bool>(true, "Review deleted successfully", true);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"Error deleting review: {ex.Message}");
                return new ApiResponse<bool>(false, "Database error occurred while deleting review");
            }
        }

        public async Task<ApiResponse<IEnumerable<ReviewDto>>> GetAllReviewsByUser()
        {
            var resultObject = await _userService.GetCurrentUser();

            if(!resultObject.Success || resultObject.User!.Username == null)
            {
                return new ApiResponse<IEnumerable<ReviewDto>>(false, "Please authenticate");
            }

            var allUserReviews = await _dbContext.Reviews.Include(r => r.Book).Where(x => x.UserId == resultObject.User.Id).ToListAsync();


            if(allUserReviews.Count() == 0)
            {
                return new ApiResponse<IEnumerable<ReviewDto>>(false, "You don't have any reviews yet");
            }

            var allUserReviewsDto = allUserReviews.Select(x => UtilityClasses.ConvertReviewToDto(x));

            return new ApiResponse<IEnumerable<ReviewDto>>(true, "Success", allUserReviewsDto);
        }

        public async Task<ApiResponse<ReviewDto>> GetReviewById(int id)
        {
            var review = await _dbContext.Reviews.Include(r => r.Book).Include(r => r.User).FirstOrDefaultAsync(x => x.Id == id);

            if(review == null)
            {
                return new ApiResponse<ReviewDto>(false, "Review not found");
            }

            return new ApiResponse<ReviewDto>(true, "Success", UtilityClasses.ConvertReviewToDto(review));
        }

        public async Task<ApiResponse<bool>> UpdateReview(int id, PostReviewDto review)
        {
            var resultObject = await _userService.GetCurrentUser();

            if (!resultObject.Success || resultObject.User!.Username == null)
            {
                return new ApiResponse<bool>(false, "Please authenticate");
            }

            var particularReview = await _dbContext.Reviews.FirstOrDefaultAsync(x => x.Id == id);

            if (particularReview == null)
            {
                return new ApiResponse<bool>(false, "Review not found");
            }

            if (particularReview.UserId != resultObject.User.Id)
            {
                return new ApiResponse<bool>(false, "Unauthorized, unable to update review");
            }

            particularReview.Comment = review.Comment;
            particularReview.Rating = review.Rating;

            _dbContext.Update(particularReview);

            try
            {
                await _dbContext.SaveChangesAsync();
                return new ApiResponse<bool>(true, "Success", true);
            } catch (Exception ex) {

                // Log the exception for debugging purposes
                Console.WriteLine($"Error deleting review: {ex.Message}");
                return new ApiResponse<bool>(false, "Database error occurred while updating review");
            }
        }
    }
}
