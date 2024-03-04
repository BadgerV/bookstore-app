using BCryptNet = BCrypt.Net.BCrypt;
using bookstore_backend.Data;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bookstore_backend.Services.Interfaces;
using bookstore_backend.models;
using bookstore_backend.Utilities;
using Microsoft.IdentityModel.Tokens;

namespace bookstore_backend.Controllers
{
   
    public class UserController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly ITokenManager _tokenManager;
        private readonly IUserService _userService;

        public UserController(BookStoreDbContext context, ITokenManager tokenManager, IUserService userService)
        {
            _context = context;
            _tokenManager = tokenManager;
            _userService = userService;
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login([FromBody] UtilityClasses.LoginRequest loginRequest)
        {
            // Manually validate the request body
            if (string.IsNullOrEmpty(loginRequest.Email) && string.IsNullOrEmpty(loginRequest.Username))
            {
                ModelState.AddModelError("Email, Username", "Email or username is required.");
            }

            var result = await _userService.Login(loginRequest.Password!, loginRequest.Email, loginRequest.Username);

            if(!result.Success)
            {
                return BadRequest(new { Success = result.Success, Message = result.Message });
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("/signup")]
        public async Task<IActionResult> SignUp([FromBody] User user )
        {
          if(!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new { Errors = errors });
            }

            var result = await _userService.Signup(user.PasswordHash, user.Email, user.Username, user.FirstName, user.LastName);

            if(!result.Success)
            {
                return BadRequest(new { Success = result.Success, Message = result.Message });
            }

            return Ok(result);
        }


        [HttpPost]
        [Route("/verify")]
        public async Task<ActionResult<bool>> VerifyToken([FromBody] string token)
        {
            if(token.IsNullOrEmpty())
            {
                ModelState.AddModelError("Token", "Please provide a valid token");
            }
            var result =  await _userService.VerifyToken(token);

            if(result == true)
            {
                return Ok(true);
            } else
            {
                return Ok(false);
            };
        }


        
    }
}
