using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserApi.DTOs;
using UserApi.Interfaces;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(SignInRequest request)
        {
            var response = await _authService.Login(request);
            if (!response.Success) return Unauthorized(response.Message);

            SetAuthCookies(response.AccessToken!);

            return Ok(response.Message);
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("token-cookies");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized("Not authorized");

            return NoContent();
        }

        private void SetAuthCookies(string accessToken)
        {
            Response.Cookies.Append("token-cookies", accessToken, new CookieOptions
            {
                Expires = DateTime.UtcNow.AddMinutes(120),
                Path = "/"
            });
        }
    }
}
