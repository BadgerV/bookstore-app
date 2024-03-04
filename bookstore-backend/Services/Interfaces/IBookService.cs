using bookstore_backend.DTOs;
using bookstore_backend.models;
using bookstore_backend.Utilities;

namespace bookstore_backend.Services.Interfaces
{
    public interface IBookService
    {
        Task<ApiResponse<IEnumerable<Book>>> GetBooks();
        Task<ApiResponse<Book>> GetBook(int id);
        Task<ApiResponse<IEnumerable<Book>>> GetBooksBySearchQuery(string searchQuery);
        Task<ApiResponse<BookDto>> AddNewBook(BookDto book);
        Task<ApiResponse<Book>> UpdateBook(Book book);
        Task<ApiResponse<bool>> DeleteBook(int id);
        Task<ApiResponse<IEnumerable<Book>>> GetBooksByTheSameAuthor(string authorUsername);
        Task<ApiResponse<IEnumerable<Book>>> GetBooksByCategory(string category);
        Task<ApiResponse<IEnumerable<Book>>> GetBooksByRecommendation();
        Task<ApiResponse<IEnumerable<Book>>> GetBooksByTopRated();
        Task<ApiResponse<IEnumerable<Book>>> GetSimilarBooks(int bookId);
        Task<ApiResponse<IEnumerable<Book>>> GetRelatedBooks(int bookId);
        Task<ApiResponse<IEnumerable<Review>>> GetReviewsForBook(int bookId);
    }
}
