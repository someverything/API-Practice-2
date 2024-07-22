using Business.Abstract;
using Business.Messages;
using Entities.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IStringLocalizer<AuthMessages> _localizer;
        public AuthController(IAuthService authService, IStringLocalizer<AuthMessages> localizer)
        {
            _authService = authService;
            _localizer = localizer;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var result = await _authService.RegisterAsync(model);
            if (result.Success)
                return Ok();
            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO model)
        {
            var result = await _authService.LoginAsync(model);
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result);
        }

        [HttpPut("{Id}")]
        //[Authorize]
        public async Task<IActionResult> LogoutAsync(string Id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new { message = _localizer["UserNotFound"] });
            }

            var result = await _authService.LogoutAsync(userId);
            if (result.Success)
            {
                return Ok(new { message = _localizer["LogoutSuccessful"] });
            }

            string response = _localizer["ErrorMessage"];
            return BadRequest(new { message = response });
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshTokenLoginAsync([FromBody] RefreshTokenDTO refreshTokenDTO)
        {
            var res = await _authService.RefreshTokenLoginAsync(refreshTokenDTO.RefreshtokenDTO);
            if(res.Success) return Ok(res);
            return BadRequest(res);

        }
    }
}
