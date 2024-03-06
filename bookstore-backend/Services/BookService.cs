using bookstore_backend.Data;
using bookstore_backend.DTOs;
using bookstore_backend.models;
using bookstore_backend.Services.Interfaces;
using bookstore_backend.Utilities;
using Microsoft.EntityFrameworkCore;

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

            var totalPages = (int)Math.Ceiling((double)bookCount / pageSize);

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


        public async Task<ApiResponse<IEnumerable<BookDto>>> GetBooksByRecommendation()
        {
            var randndomBooks = await _dbContext.Books.OrderBy(x => Guid.NewGuid()).Take(10).ToListAsync();

            var randomBookDTOS = randndomBooks.Select(book => UtilityClasses.ConvertBookToDto(book));

            return new ApiResponse<IEnumerable<BookDto>>
            (
                true,
                "Success",
                randomBookDTOS
            );
        }

        public async Task<ApiResponse<IEnumerable<BookDto>>> GetBooksBySearchQuery(string searchQuery, int page = 1, int pageSize = 10)
        {
            var booksCount = await _dbContext.Books.CountAsync();

            var totalPages = (int)Math.Ceiling((double)booksCount / pageSize);

            if (page > totalPages)
            {
                return new ApiResponse<IEnumerable<BookDto>>(false, "Maximum page exceeded");
            }

            var foundBooks = await _dbContext.Books.Where(book => book.Title.ToLower().Contains(searchQuery.ToLower())).OrderBy(x => x.Title).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            if (foundBooks.Count == 0)
            {
                return new ApiResponse<IEnumerable<BookDto>>(false, "No books found");
            }

            var foundBookDTOs = foundBooks.Select(x => UtilityClasses.ConvertBookToDto(x));

            return new ApiResponse<IEnumerable<BookDto>>(true, "Success", foundBookDTOs);
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

        public async Task<ApiResponse<IEnumerable<ReviewDto>>> GetReviewsForBook(int bookId)
        {
            var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == bookId);

            if(book == null)
            {
                return new ApiResponse<IEnumerable<ReviewDto>>(false, "Book not found");
            }

            var reviewsForBook = await _dbContext.Reviews.Include(x => x.User).Include(x => x.Book).Where(review => review.BookId == bookId).ToListAsync();


            if(reviewsForBook.Count == 0)
            {
                return new ApiResponse<IEnumerable<ReviewDto>>(false, "This book has no reviews yet");
            }

            var reviewForBooksDto = reviewsForBook.Select(bookreview => UtilityClasses.ConvertReviewToDto(bookreview));

            return new ApiResponse<IEnumerable<ReviewDto>>(true, "Success", reviewForBooksDto);
        }

        public Task<ApiResponse<IEnumerable<BookDto>>> GetSimilarBooks(int bookId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<int>> GetAverageRatingForBook(int bookId)
        {
            var ratingReviewsForBook = await _dbContext.Reviews.Where(review => review.BookId == bookId).Select(x => x.Rating).ToListAsync();

            if(ratingReviewsForBook == null)
            {
                return new ApiResponse<int>(false, "Book not found");
            }

            if (ratingReviewsForBook.Count() == 0)
            {
                return new ApiResponse<int>(false, "This book has no ratings yet");
            }

            var averagveRating = (int)ratingReviewsForBook.Sum() / ratingReviewsForBook.Count();

            return new ApiResponse<int>(true, "Success", averagveRating);
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
