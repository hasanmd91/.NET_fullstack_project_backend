using Ecom.Core.src.parameters;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.Service;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Controller.src.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IUserService userService, IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost()]
        public async Task<ActionResult<string>> Login([FromBody] Credentials credentials)
        {

            return await _authService.Login(credentials);
        }


    }
}