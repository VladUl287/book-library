using Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using book_library_backend.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Common.Extensions;

namespace book_library_backend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private const string REFRESH_TOKEN = "refresh_token";

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] AuthModel authModel)
        {
            var loginResult = await authService.Login(authModel);

            return loginResult.Match<IActionResult>(
                success =>
                {
                    Response.Cookies.Append(REFRESH_TOKEN, success.RefreshToken, new CookieOptions
                    {
                        Expires = new DateTimeOffset(DateTime.UtcNow.AddDays(30))
                    });

                    return Ok(success);
                },
                error => BadRequest(error));
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] AuthModel authModel)
        {
            var registerResult = await authService.Register(authModel);

            return registerResult.Match<IActionResult>(
                success => Created("Register", success),
                error => BadRequest(error));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies[REFRESH_TOKEN];
            var userId = User.GetLoggedInUserId<Guid>();

            if (!string.IsNullOrEmpty(refreshToken) && userId != default)
            {
                Response.Cookies.Delete(REFRESH_TOKEN);

                await authService.Logout(userId);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies[REFRESH_TOKEN];

            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized();
            }

            var userId = User.GetLoggedInUserId<Guid>();

            var refreshResult = await authService.Refresh(userId, refreshToken);
            return refreshResult.Match<IActionResult>(
                success =>
                {
                    Response.Cookies.Append(REFRESH_TOKEN, success.RefreshToken, new CookieOptions
                    {
                        Expires = new DateTimeOffset(DateTime.UtcNow.AddDays(30))
                    });

                    return Ok(success);
                },
                error => BadRequest(error));
        }
    }
}
