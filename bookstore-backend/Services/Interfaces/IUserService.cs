using bookstore_backend.DTOs;
using bookstore_backend.Utilities;

namespace bookstore_backend.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserAuthenticationresult> Signup(string password, string email, string username, string firstname, string lastname);
        Task<UserAuthenticationresult> Login(string username, string password);
        Task<bool> VerifyToken(string token);
        Task<UserAuthenticationresult> BecomeAuthor(string phoneNumer, DateOnly dateOfBirth, string billingAddress);
        int GetAuthenticatedUserId();
        Task<UserAuthenticationresult> GetCurrentUser();
    }
}
