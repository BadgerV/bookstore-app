using Microsoft.AspNetCore.Mvc;
using bookstore_backend.Services.Interfaces;
using bookstore_backend.models;
using bookstore_backend.Utilities;
using Microsoft.IdentityModel.Tokens;
using bookstore_backend.DTOs;

namespace bookstore_backend.Controllers
{

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await _userService.Login(loginRequest.Username, loginRequest.Password);

            if (!result.Success)
            {
                return BadRequest(new { Success = result.Success, Message = result.Message });
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("/signup")]
        public async Task<IActionResult> SignUp([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new { Errors = errors });
            }

            var result = await _userService.Signup(user.PasswordHash!, user.Email!, user.Username!, user.FirstName!, user.LastName!);

            if (!result.Success)
            {
                return BadRequest(new { Success = result.Success, Message = result.Message });
            }

            return Ok(result);
        }


        [HttpPost]
        [Route("/verify")]
        public async Task<ActionResult<bool>> VerifyToken([FromBody] string token)
        {
            if (token.IsNullOrEmpty())
            {
                ModelState.AddModelError("Token", "Please provide a valid token");
            }
            var result = await _userService.VerifyToken(token);

            if (result == true)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            };
        }


        [HttpPost]
        [Route("/become-author")]
        public async Task<IActionResult> BecomeAuthor([FromBody] UtilityClasses.BecomeAuthorInfos infos)
        {
            if (infos.billingAddress == null || infos.phoneNumber == null)
            {
                return BadRequest("Please provide all the fields");
            }
            var result = await _userService.BecomeAuthor(infos.phoneNumber, infos.dateOfBirth, infos.billingAddress);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}
