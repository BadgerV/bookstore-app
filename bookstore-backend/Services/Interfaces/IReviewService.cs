using bookstore_backend.DTOs;
using bookstore_backend.Utilities;

namespace bookstore_backend.Services.Interfaces
{
    public interface IReviewService
    {
        Task<ApiResponse<bool>> AddReview(int bookId, PostReviewDto review);
        Task<ApiResponse<ReviewDto>> GetReviewById(int id);
        Task<ApiResponse<IEnumerable<ReviewDto>>> GetAllReviewsByUser();
       Task<ApiResponse<bool>> UpdateReview(int id, PostReviewDto review);
       Task<ApiResponse<bool>> DeleteReview(int id);
    }
}
