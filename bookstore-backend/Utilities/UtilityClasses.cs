using bookstore_backend.DTOs;
using bookstore_backend.models;
using System.ComponentModel.DataAnnotations;
using BCryptNet = BCrypt.Net.BCrypt;

namespace bookstore_backend.Utilities;

public static class UtilityClasses
{
    public static bool ValidatePassword(string plainPassword, string hashedPassword)
    {
        return BCryptNet.Verify(plainPassword, hashedPassword);
    }

    public static string HashPassword(string password)
    {
        return BCryptNet.HashPassword(password);
    }

    public static UserDto MapToUserDto(User user) => new()
    {
        Username = user.Username,
        Email = user.Email,
        FirstName = user.FirstName,
        LastName = user.LastName,
        ShippingAddress = user.ShippingAddress,
        IsAuthor = user.IsAuthor || false,
    };

    public class BecomeAuthorInfos
    {
        [Required(ErrorMessage = "Please provide your phone number")]
        public string? phoneNumber { get; set; }

        [Required(ErrorMessage = "Please provide your billing address")]
        public string? billingAddress { get; set; }

        [Required(ErrorMessage = "Please provide your date of birth")]
        public DateOnly dateOfBirth { get; set;}
    }

    public static Book ConvertToBook(BookDto bookDto)
    {
        return new Book
        {
            Title = bookDto.Title ?? string.Empty,
            Author = bookDto.Author ?? string.Empty,
            Description = bookDto.Description,
            Image = bookDto.Image,
            
            Price = bookDto.Price,
            // Assuming other properties like Category, AverageRating, and Reviews are not part of the DTO
            // They can be populated from the database later if needed
        };
    }

    public static BookDto ConvertBookToDto(Book book)
    {
        return new BookDto
        {
            Title = book.Title,
            Author = book.Author,
            Description = book.Description,
            Image = book.Image,
            Price = book.Price
        };
    }

    public static ReviewDto ConvertReviewToDto(Review review)
    {
        return new ReviewDto
        {
            UserUsername = review.User!.Username,
            BookName = review.Book!.Title,
            Comment = review.Comment,
            Rating = review.Rating,
        };
    }
}
