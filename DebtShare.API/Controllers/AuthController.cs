using DebtShare.API.Services;
using Microsoft.AspNetCore.Mvc;
using DebtShare.API.InputModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DebtShare.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterInputModel inputModel)
        {
            var result = await _authService.RegisterAsync(inputModel);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("User created successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginInputModel inputModel)
        {
            var tokens = await _authService.LoginAsync(inputModel);

            if (tokens == null)
            {
                return Unauthorized("Invalid email or password");
            }

            return Ok(tokens);
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh([FromBody] string oldRefreshToken)
        {
            var tokens = await _authService.RefreshTokenAsync(oldRefreshToken);

            if (tokens == null)
            {
                return Unauthorized("Invalid refresh token.");
            }

            return Ok(new
            {
                tokens.AccessToken,
                tokens.RefreshToken
            });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] string refreshTokenValue)
        {
            // userId вземаме от Claims (примерно NameIdentifier),
            // ако ползвате Identity, това може да е user.Id
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized("No user info found in token.");

            var success = await _authService.LogoutAsync(refreshTokenValue, userId);

            if (!success)
                return BadRequest("Invalid refresh token or does not belong to the user.");

            return Ok("You have been logged out successfully.");
        }


        [Authorize]
        [HttpGet("secure-data")]
        public IActionResult GetSecureData()
        {

            return Ok("Това го виждат само логнати потребители!");
        }
    }
}
