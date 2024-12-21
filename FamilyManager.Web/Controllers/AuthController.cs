using FamilyManager.Application.RegisterModels.Commands;
using FamilyManager.Domain.Entities;
using FamilyManager.Infrastructure.Identity;
using FamilyManager.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FamilyManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly TokenProvider _tokenProvider;
        private readonly IMediator _mediator;
        public AuthController(UserManager<User> userManager, TokenProvider tokenProvider, IMediator mediator)
        {
            _userManager = userManager;
            _tokenProvider = tokenProvider;
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Guid>> Register([FromBody] CreateRegisterModelCommand model)
        {
            try
            {
                var registerModelId = await _mediator.Send(model);

                if (registerModelId == Guid.Empty)
                {
                    return BadRequest("An error occured!");
                }

                return Ok(registerModelId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return Unauthorized();
            }
            var isValidPassword = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!isValidPassword)
            {
                return Unauthorized();
            }

            var roles = await _userManager.GetRolesAsync(user);

            var token = _tokenProvider.GenerateJwtToken(user, roles);
            var refreshToken = await _tokenProvider.GenerateRefreshToken(user.Id);

            return Ok(new { Token = token, RefreshToken = refreshToken });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel model)
        {
            try
            {
                var (newJwtToken, newRefreshToken) = await _tokenProvider.RefreshTokens(model.RefreshToken);

                return Ok(new { Token = newJwtToken, RefreshToken = newRefreshToken });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }
        }
    }
}
