using BCryptNet = BCrypt.Net.BCrypt;
using bookstore_backend.Data;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bookstore_backend.Services.Interfaces;
using bookstore_backend.models;

namespace bookstore_backend.Controllers
{
    public class LoginRequest
    {
        public string Email { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
    public class UserController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly ITokenManager _tokenManager;

        public UserController(BookStoreDbContext context, ITokenManager tokenManager)
        {
            _context = context;
            _tokenManager = tokenManager;
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var userFromEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginRequest.Email);
            var userFromUsername = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginRequest.Username);

            if(userFromEmail != null)
            {
                if (!ValidatePassword(userFromEmail.PasswordHash!, loginRequest.Password))
                {
                    return BadRequest("Invalid username or password");
                }

                var tokenString = _tokenManager.GenerateJWTToken(userFromEmail);

                return Ok(new { token = tokenString, userFromEmail });
            } else if (userFromUsername != null)
            {
                if (!ValidatePassword(userFromUsername.PasswordHash!, loginRequest.Password))
                {
                    return BadRequest("Invalid username or password");
                }

                var tokenString = _tokenManager.GenerateJWTToken(userFromUsername);

                return Ok(new { token = tokenString, userFromUsername });
            } else
            {
                return BadRequest("Invalid credentials");
            }
        }

        [HttpPost]
        [Route("/signup")]
        public async Task<IActionResult> SignUp([FromBody] User user )
        {
          if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(_context.Users.Any(x => x.Email == user.Email))
            {
                return BadRequest("Email already in use");
            }

            var hashedPassword = BCryptNet.HashPassword(user.PasswordHash);

            var newUser = new User
            {
                Username = user.Username,
                Email = user.Email,
                PasswordHash = hashedPassword,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            await _context.AddAsync(newUser);

            await _context.SaveChangesAsync();


            var tokenString = _tokenManager.GenerateJWTToken(newUser);

            // Return the token in the response
            return Ok(new { token = tokenString, newUser });
        }


        private bool ValidatePassword(string hashedPassword, string plainPassword)
        {
            return BCryptNet.Verify(plainPassword, hashedPassword);
        }
    }
}
