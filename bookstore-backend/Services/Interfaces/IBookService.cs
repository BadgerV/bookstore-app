using bookstore_backend.DTOs;
using bookstore_backend.Utilities;

namespace bookstore_backend.Services.Interfaces;

public interface IBookService
{
    Task<ApiResponse<IEnumerable<BookDto>>> GetBooks(int page, int pageSize);
    Task<ApiResponse<BookDto>> GetBook(int id);
    Task<ApiResponse<IEnumerable<BookDto>>> GetBooksBySearchQuery(string searchQuery, int page = 1, int pageSize = 10);
    Task<ApiResponse<CreateBookResponseDto>> AddNewBook(CreateBookDto book);
    Task<ApiResponse<BookDto>> UpdateBook(CreateBookDto book, int id);
    Task<ApiResponse<bool>> DeleteBook(int id);
    Task<ApiResponse<IEnumerable<BookDto>>> GetBooksByTheSameAuthor(string authorUsername, int page  = 1, int pageSize = 10);
    Task<ApiResponse<IEnumerable<BookDto>>> GetBooksByCategory(string category, int page, int pageSize);
    Task<ApiResponse<IEnumerable<BookDto>>> GetBooksByRecommendation();
    Task<ApiResponse<IEnumerable<BookDto>>> GetBooksByTopRated();
    Task<ApiResponse<IEnumerable<BookDto>>> GetSimilarBooks(int bookId);
    Task<ApiResponse<IEnumerable<BookDto>>> GetRelatedBooks(int bookId);
    Task<ApiResponse<IEnumerable<ReviewDto>>> GetReviewsForBook(int bookId);
    Task<ApiResponse<int>> GetAverageRatingForBook(int bookId);
}