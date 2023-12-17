using Ecom.Core.src.Enum;
using Ecom.Core.src.parameters;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Controller.src.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // [Authorize(Roles = "Admin")]
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetAllUserAsync([FromQuery] GetAllParams options)
        {
            return Ok(await _userService.GetAllUserAsync(options));
        }


        // [Authorize]
        [HttpGet("{useriId}")]
        public async Task<ActionResult<UserReadDTO>> GetOneUserByIdAsync(Guid useriId)
        {
            return Ok(await _userService.GetOneUserByIdAsync(useriId));
        }


        // [Authorize(Roles = "User")]
        [HttpPost()]
        public async Task<ActionResult<UserReadDTO>> CreateOneUserAsync([FromBody] UserCreateDTO userCreateDTO)
        {
            var createdUser = await _userService.CreateOneUserAsync(userCreateDTO);
            return Ok(createdUser);
        }



        // [Authorize(Roles = "User")]
        [HttpPatch("{userid}")]
        public async Task<ActionResult<UserReadDTO>> UpdateOneUserAsync(Guid userId, [FromBody] UserUpdateDTO userUpdateDTO)
        {
            return Ok(await _userService.UpdateOneUserAsync(userId, userUpdateDTO));
        }


        // [Authorize(Roles = "Admin")]
        [HttpDelete("{userId}")]
        public async Task<ActionResult<bool>> DeleteOneUserAsync(Guid userId)
        {
            return StatusCode(204, await _userService.DeleteOneUserAsync(userId));
        }
    }
}