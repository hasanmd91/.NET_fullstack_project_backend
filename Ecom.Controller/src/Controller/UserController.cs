using Ecom.Core.src.Enum;
using Ecom.Core.src.parameters;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.DTO;
using Microsoft.AspNetCore.Authorization;
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
        public ActionResult<IEnumerable<UserReadDTO>> GetAll([FromQuery] GetAllParams options)
        {
            return Ok(_userService.GetAll(options));
        }


        // [Authorize]
        [HttpGet("{useriId}")]
        public ActionResult<UserReadDTO> GetOne(Guid useriId)
        {
            return Ok(_userService.GetOne(useriId));
        }


        // [Authorize(Roles = "User")]
        [HttpPost()]
        public ActionResult<UserReadDTO> CreateOne([FromBody] UserCreateDTO userCreateDTO)
        {
            return CreatedAtAction(nameof(CreateOne), _userService.CreateOne(userCreateDTO));
        }


        // [Authorize(Roles = "User")]
        [HttpPatch("{userid}")]
        public ActionResult<UserReadDTO> UpdateOne(Guid userId, [FromBody] UserUpdateDTO userUpdateDTO)
        {
            return Ok(_userService.UpdateOne(userId, userUpdateDTO));
        }


        // [Authorize(Roles = "Admin")]
        [HttpDelete("{userId}")]
        public ActionResult<bool> DeleteOneById(Guid userId)
        {
            return StatusCode(204, _userService.DeleteOneById(userId));
        }
    }
}