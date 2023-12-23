using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Ecom.Core.src.parameters;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.DTO;
using Ecom.Service.src.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;


namespace Ecom.Controller.src.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IUserService userService, IAuthService authService)
        {
            _authService = authService;
            _userService = userService;
        }


        [HttpPost()]
        public async Task<ActionResult<string>> Login([FromBody] Credentials credentials)
        {

            return await _authService.Login(credentials);
        }


        [HttpGet("profile")]
        public async Task<ActionResult<UserReadDTO>> GetCurrentUserProfile()
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(accessToken))
            {
                return Unauthorized("Access token not found in the Authorization header.");
            }

            var handler = new JwtSecurityTokenHandler();

            if (handler.ReadToken(accessToken) is not JwtSecurityToken jsonToken)
            {
                return BadRequest("Invalid access token format.");
            }
            var userIdClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;

            if (userIdClaim == null)
            {
                return Unauthorized("User ID is not found in the token.");
            }

            if (!Guid.TryParse(userIdClaim, out Guid userId))
            {
                return BadRequest("Invalid user ID in token.");
            }

            var userProfile = await _userService.GetOneUserByIdAsync(userId);
            return Ok(userProfile);

        }

    }

}