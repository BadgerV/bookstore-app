using bookstore_backend.Data;
using bookstore_backend.DTOs;
using bookstore_backend.models;
using bookstore_backend.Services.Interfaces;
using bookstore_backend.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Security.Claims;

namespace bookstore_backend.Services
{
    public class UserService : IUserService
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly ITokenManager _tokenManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(BookStoreDbContext dbContext, ITokenManager tokenManager, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _tokenManager = tokenManager;
            _httpContextAccessor = httpContextAccessor;
        }

        

        public async Task<UserAuthenticationresult> Login(string password, string? email, string? username)
        {
            if(email != null && username != null)
            {
                return new UserAuthenticationresult
                {
                    Success = false,
                    Message = "Please provide only email or username not both"
                };
            }
            var userFromEmail = await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
            var userFromUsername = await _dbContext.Users.FirstOrDefaultAsync(user => user.Username == username);

            if(userFromUsername == null && userFromEmail == null) {
                return new UserAuthenticationresult
                {
                    Success = false,
                    Message = "Invalid credentials. Please try again"
                };
            } else if(userFromUsername != null)
            {
                if(!UtilityClasses.ValidatePassword(password, userFromUsername.PasswordHash))
                {
                    return new UserAuthenticationresult
                    {
                        Message = "Invalid credentials",
                        Success = false
                    };
                } else
                {
                    var token = _tokenManager.GenerateJWTToken(userFromUsername);
                    return new UserAuthenticationresult
                    {
                        Success = true,
                        Message = "Success",
                        User = UtilityClasses.MapToUserDto(userFromUsername),
                        Token = token,
                    };
                }
            }
            else if(userFromEmail != null)
            {
                if (!UtilityClasses.ValidatePassword(password, userFromEmail.PasswordHash))
                {
                    return new UserAuthenticationresult
                    {
                        Message = "Invalid credentials",
                        Success = false
                    };
                } else
                {
                    var token = _tokenManager.GenerateJWTToken(userFromEmail);
                    return new UserAuthenticationresult
                    {
                        Success = true,
                        Message = "Success",
                        User = UtilityClasses.MapToUserDto(userFromEmail),
                        Token = token,
                    };
                }
            }
            else
            {
                return new UserAuthenticationresult
                {
                    Success = false,
                    Message = "Something went wrong"
                };
            }
           
        }

        public async Task<UserAuthenticationresult> Signup(string password, string email, string username, string firstname, string lastname)
        {
            User? userExistsWithEmail = await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
            User? userExistsWithUsername = await _dbContext.Users.FirstOrDefaultAsync(user => user.Username == username);

            if (userExistsWithUsername != null)
            {
                return new UserAuthenticationresult
                {
                    Success = false,
                    Message = "A user with this username already exists please use another username"
                };
            }

            if (userExistsWithEmail != null)
            {
                return new UserAuthenticationresult
                {
                    Success = false,
                    Message = "A user with this email already exists please use another email"

                };
            }



            User newUser = new User
            {
                FirstName = firstname,
                LastName = lastname,
                PasswordHash = UtilityClasses.HashPassword(password),
                Username = username,
                Email = email,
            };

            await _dbContext.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();

            string tokenString = _tokenManager.GenerateJWTToken(newUser);

            return new UserAuthenticationresult
            {
                Success = true,
                Message = "Success",
                User = UtilityClasses.MapToUserDto(newUser),
                Token = tokenString
            };
        }

        public async Task<bool> VerifyToken(string token)
        {
            try
            {
                // Attempt token validation
                ClaimsPrincipal claimsPrincipal =  _tokenManager.ValidateToken(token);

                // Check if validation was successful
                if (claimsPrincipal != null)
                {
                    // Successful validation, consider logging or returning additional details
                    return true;
                }
                else
                {
                    // Validation failed, log or return error message
                    await Console.Out.WriteLineAsync("Token validation failed: ClaimsPrincipal is null");
                    return false;
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return false;
            }
        }

        public int GetAuthenticatedUserId()
        {
            ClaimsPrincipal currentUser = _httpContextAccessor.HttpContext!.User;

            Claim? userIdClaim = currentUser.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                // If userIdClaim is null, return a default value (0 in this case, assuming user IDs are positive integers)
                return -1;
            }
            else
            {
                // Parse the value of userIdClaim to an integer and return it
                return int.Parse(userIdClaim.Value);
            }
        }

        

        

        public async Task<UserAuthenticationresult> BecomeAuthor(string phoneNumer, DateOnly dateOfBirth, string billingAddress)
        {
            var userId = GetAuthenticatedUserId();

            if (userId == -1)
            {
                return new UserAuthenticationresult
                {
                    Success = false,
                    Message = "Please authenticate"
                };
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);

            if (user == null)
            {
                return new UserAuthenticationresult
                {
                    Message = "Something went wrong",
                    Success = false
                };
            }

            if (user.IsAuthor == true)
            {
                return new UserAuthenticationresult
                {
                    Success = false,
                    Message = "You are already an author"
                };
            }

            user.PhoneNumber = phoneNumer;
            user.DateOfBirth = dateOfBirth;
            user.BillingAddress = billingAddress;
            user.UpdatedAt = DateTime.UtcNow;
            user.IsAuthor = true;

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            return new UserAuthenticationresult
            {
                Success = true,
                Message = "Success",
                User = UtilityClasses.MapToUserDto(user)
            };

        }

        public async Task<UserAuthenticationresult> GetCurrentUser()
        {
            int userId = GetAuthenticatedUserId();
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);

            if(user == null)
            {
                return new UserAuthenticationresult
                {
                    Success = false,
                    Message = "User does not exist",
                };
            }

            return new UserAuthenticationresult
            {
                Success = true,
                Message = "Success",
                User = UtilityClasses.MapToUserDto(user)
            };
        }
    }
}
