using bookstore_backend.Data;
using bookstore_backend.DTOs;
using bookstore_backend.models;
using bookstore_backend.Services.Interfaces;
using bookstore_backend.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

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

        public async Task<ApiResponse<Book>> AddNewBook(CreateBookDto book)
        {
            if (string.IsNullOrEmpty(book.Title) || book == null)
            {
                return new ApiResponse<Book>(false, "Please provide all the fields");
            }


            var resultObject = await _userService.GetCurrentUser();

            if(resultObject.Success == false)
            {
                return new ApiResponse<Book>(false, "User not found");
            }

            if(resultObject.User?.IsAuthor == false)
            {
                return new ApiResponse<Book>(false, "You are not an author");
            }

            var categories = await _dbContext.Categories.Where(cat => book.CategoriesId!.Contains(cat.Id)).Select(cat => cat.Name).ToListAsync();

            Book newBook = new()
            {
                Title = book.Title,
                Author = resultObject.User!.Username!,
                Description = book.Description,
                Price = book.Price
            };

            newBook.Categories.AddRange(categories);

            await _dbContext.Books.AddAsync(newBook);
            await _dbContext.SaveChangesAsync();

            return new ApiResponse<Book>(true, "Success", newBook);
        }

        public async Task<ApiResponse<bool>> DeleteBook(int id)
        {
            var currentUser = await _userService.GetCurrentUser();
            var foundBook = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (!currentUser.Success)
            {
                return new ApiResponse<bool>(false, "Please authenticate");
            }

            if (currentUser.User != null && currentUser.User.IsAuthor == true && foundBook != null && foundBook.Author == currentUser.User.Username)
            {
                try
                {
                    _dbContext.Books.Remove(foundBook);
                    await _dbContext.SaveChangesAsync();
                    return new ApiResponse<bool>(true, "Success", true);
                }
                catch (Exception ex)
                {
                    // Log the exception
                    return new ApiResponse<bool>(false, ex.Message);
                }
            }
            else
            {
                return new ApiResponse<bool>(false, "Unauthorized");
            }
        }


        public async Task<ApiResponse<BookDto>> GetBook(int id)
        {
            if (id <= 0)
            {
                return new ApiResponse<BookDto>(false, "Id cannot be less than zero");
            }

            var foundBook = await _dbContext.Books.FirstOrDefaultAsync(book => book.Id == id);

            if (foundBook == null)
            {
                return new ApiResponse<BookDto>(false, "Book not found for the given id");
            }

            return new ApiResponse<BookDto>(true, "Success", UtilityClasses.ConvertBookToDto(foundBook));
        }

        public async Task<ApiResponse<IEnumerable<BookDto>>> GetBooks(int page = 1, int pageSize = 10)
        {
            if(page <= 0 || pageSize <=0)
            {
                return new ApiResponse<IEnumerable<BookDto>>(false, "Page size or page number cannot be less than 1");
            }

            var bookCount = await _dbContext.Books.CountAsync();

            var totalPages = (int)Math.Ceiling((double)bookCount / page);

            if(page > totalPages)
            {
                return new ApiResponse<IEnumerable<BookDto>>(false, "Page number exceeded");
            }


            var books = await _dbContext.Books
            .OrderBy(b => b.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

            var bookDtos = books.Select(book => UtilityClasses.ConvertBookToDto(book));

            return new ApiResponse<IEnumerable<BookDto>>(true, "Success", bookDtos);
        }

        public async Task<ApiResponse<IEnumerable<BookDto>>> GetBooksByCategory(string category, int page = 1, int pageSize = 10)
        {
            if (string.IsNullOrEmpty(category))
            {
                return new ApiResponse<IEnumerable<BookDto>>(false, "Please provide a category");
            }

            var foundCategory = await _dbContext.Categories.FirstOrDefaultAsync(cat => cat.Name.ToLower() == category.ToLower());

            if (foundCategory == null)
            {
                return new ApiResponse<IEnumerable<BookDto>>(false, "Category not found");
            }

            var foundBooks = await _dbContext.Books
             .Where(book => book.Categories!.Any(c => c == foundCategory.Name))
             .Skip((page - 1) * pageSize)
             .Take(pageSize)
             .ToListAsync();


            var bookDtos = foundBooks.Select(book => UtilityClasses.ConvertBookToDto(book));

            return new ApiResponse<IEnumerable<BookDto>>(true, "Success", bookDtos);
        }


        public Task<ApiResponse<IEnumerable<BookDto>>> GetBooksByRecommendation()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IEnumerable<BookDto>>> GetBooksBySearchQuery(string searchQuery)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<IEnumerable<BookDto>>> GetBooksByTheSameAuthor(string authorUsername, int page = 1, int pageSize = 10)
        {
            var author = await _dbContext.Users.FirstOrDefaultAsync(user => user.Username == authorUsername);
            
            if (author == null)
            {
                return new ApiResponse<IEnumerable<BookDto>>(false, "Author not found");
            }

            var authorBooks = await _dbContext.Books.Where(book => book.Author.ToLower() == author.Username.ToLower()).ToListAsync();

            if(authorBooks.Count ==0)
            {
                return new ApiResponse<IEnumerable<BookDto>>(false, "Author does not have books yet");
            }

            var authorBookDtos = authorBooks.Select(book => UtilityClasses.ConvertBookToDto(book));

            return new ApiResponse<IEnumerable<BookDto>>(true, "Success", authorBookDtos);
        }

        public Task<ApiResponse<IEnumerable<BookDto>>> GetBooksByTopRated()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IEnumerable<BookDto>>> GetRelatedBooks(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IEnumerable<Review>>> GetReviewsForBook(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IEnumerable<BookDto>>> GetSimilarBooks(int bookId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<BookDto>> UpdateBook(CreateBookDto book, int id)
        {
            var currentUser = await _userService.GetCurrentUser();
            var foundBook = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (!currentUser.Success)
            {
                return new ApiResponse<BookDto>(false, "Please authenticate");
            }

            if (currentUser.User != null && currentUser.User.IsAuthor == true && foundBook != null && foundBook.Author == currentUser.User.Username)
            {
                try
                {
                    var categories = await _dbContext.Categories.Where(cat => book.CategoriesId!.Contains(cat.Id)).Select(cat => cat.Name).ToListAsync();

                    // Update properties of the found book
                    foundBook.Title = book.Title!;
                    foundBook.Description = book.Description;
                    foundBook.Price = book.Price;
                    foundBook.Categories = categories; // Assuming Categories is a list of strings

                    await _dbContext.SaveChangesAsync();

                    var updatedBook = await _dbContext.Books.FirstOrDefaultAsync(book => book.Id == id);
                    return new ApiResponse<BookDto>(true, "Success", UtilityClasses.ConvertBookToDto(updatedBook!));
                }
                catch (Exception ex)
                {
                    // Log the exception
                    return new ApiResponse<BookDto>(false, ex.Message);
                }
            }
            else
            {
                return new ApiResponse<BookDto>(false, "Unauthorized");
            }
        }
    }
}
