using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.DTO;
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

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrderAsync(GetAllParams options)
        {
            Console.WriteLine("controller was reached");
            var result = await _orderService.GetAllOrderAsync(options);
            return Ok(result);
        }

        [HttpPost()]
        public async Task<ActionResult<OrderReadDTO>> CreateOrderAsync([FromBody] OrderCreateDTO orderCreateDTO)
        {
            var result = await _orderService.CreateOrderAsync(orderCreateDTO);
            return Ok(result);
        }

    }
}