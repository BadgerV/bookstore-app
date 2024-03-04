using bookstore_backend.Data;
using bookstore_backend.DTOs;
using bookstore_backend.models;
using bookstore_backend.Services.Interfaces;
using bookstore_backend.Utilities;

namespace bookstore_backend.Services
{
    public class BookService : IBookService
    {
        private readonly IUserService _userService;
        private readonly BookStoreDbContext _dbContext;

        public BookService(IUserService userService, BookStoreDbContext bookStoreDb)
        {   
            _userService = userService;
            _dbContext = bookStoreDb;
        }

        public async Task<ApiResponse<BookDto>> AddNewBook(BookDto book)
        {
            var resultObject = await _userService.GetCurrentUser();

            if(resultObject.Success == false)
            {
                return new ApiResponse<BookDto>(false, "User not found");
            }

            if(resultObject.User?.IsAuthor == false)
            {
                return new ApiResponse<BookDto>(false, "You are not an author");
            }

            book.Author = resultObject.User?.Username!;

            await _dbContext.Books.AddAsync(UtilityClasses.ConvertToBook(book)!);
            await _dbContext.SaveChangesAsync();

            return new ApiResponse<BookDto>(true, "Success", book);
        }

        public Task<ApiResponse<bool>> DeleteBook(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<Book>> GetBook(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IEnumerable<Book>>> GetBooks()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IEnumerable<Book>>> GetBooksByCategory(string category)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IEnumerable<Book>>> GetBooksByRecommendation()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IEnumerable<Book>>> GetBooksBySearchQuery(string searchQuery)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IEnumerable<Book>>> GetBooksByTheSameAuthor(string authorUsername)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IEnumerable<Book>>> GetBooksByTopRated()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IEnumerable<Book>>> GetRelatedBooks(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IEnumerable<Review>>> GetReviewsForBook(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IEnumerable<Book>>> GetSimilarBooks(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<Book>> UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
