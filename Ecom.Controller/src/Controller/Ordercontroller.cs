using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Controller.src.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class Ordercontroller : ControllerBase
    {
        private readonly IOrderService _orderService;

        public Ordercontroller(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<OrderReadDTO>>> GetAllOrderAsync([FromQuery] GetAllParams options)
        {
            var result = await _orderService.GetAllOrderAsync(options);
            return Ok(result);
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet("{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderReadDTO>>> GetOneOrderAsync(Guid orderId)
        {
            var result = await _orderService.GetOneOrderAsync(orderId);
            return Ok(result);
        }

        [Authorize(Roles = "User")]
        [HttpPost()]
        public async Task<ActionResult<OrderReadDTO>> CreateOrderAsync([FromBody] OrderCreateDTO orderCreateDTO)
        {
            var result = await _orderService.CreateOrderAsync(orderCreateDTO);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{orderId}")]
        public async Task<ActionResult<bool>> DeleteOrderAsync(Guid orderId)
        {
            var result = await _orderService.DeleteOrderAsync(orderId);
            return StatusCode(204, result);
        }


        [Authorize(Roles = "Admin")]
        [HttpPatch("{orderId}")]
        public async Task<ActionResult<OrderReadDTO>> UpdateOrderAsync(Guid orderId, OrderUpdateDTO orderUpdateDTO)
        {
            var result = await _orderService.UpdateOrderAsync(orderId, orderUpdateDTO);
            return Ok(result);
        }

    }
}