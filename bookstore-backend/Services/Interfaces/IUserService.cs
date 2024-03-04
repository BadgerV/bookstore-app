using bookstore_backend.Utilities;

namespace bookstore_backend.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserAuthenticationresult> Signup(string password, string email, string username, string firstname, string lastname);
        Task<UserAuthenticationresult> Login(string password, string? email, string? username);
        Task<bool> VerifyToken(string token);
    }
}
