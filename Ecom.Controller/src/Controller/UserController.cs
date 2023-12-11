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

        [HttpGet()]
        public ActionResult<IEnumerable<UserReadDTO>> GetAll([FromQuery] GetAllParams options)
        {
            return Ok(_userService.GetAll(options));
        }

        [HttpGet("{useriId}")]
        public ActionResult<UserReadDTO> GetOne(Guid useriId)
        {
            return Ok(_userService.GetOne(useriId));
        }

        [HttpPost()]
        public ActionResult<UserReadDTO> CreateOne([FromBody] UserCreateDTO userCreateDTO)
        {
            return CreatedAtAction(nameof(CreateOne), _userService.CreateOne(userCreateDTO));
        }

        [HttpPut("{userid}")]
        public ActionResult<UserReadDTO> UpdateOne(Guid userId, [FromBody] UserUpdateDTO userUpdateDTO)
        {
            return Ok(_userService.UpdateOne(userId, userUpdateDTO));
        }


        [HttpDelete("{userId}")]

        public ActionResult<bool> DeleteOne(Guid userId)
        {
            return Ok(_userService.DeleteOne(userId));
        }



    }
}