using System.Security.Claims;
using Ecom.Core.src.Enum;
using Ecom.Core.src.parameters;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.DTO;
using Ecom.Service.src.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Ecom.Controller.src.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class Ordercontroller : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IAuthorizationService _authorizationService;

        public Ordercontroller(IOrderService orderService, IAuthorizationService authorizationService)
        {
            _orderService = orderService;
            _authorizationService = authorizationService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<OrderReadDTO>>> GetAllOrderAsync([FromQuery] GetAllParams options)
        {
            var result = await _orderService.GetAllOrderAsync(options);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderReadDTO>>> GetOneOrderAsync(Guid orderId)
        {
            var order = await _orderService.GetOneOrderAsync(orderId) ?? throw CustomException.NotFoundException("order not found");

            var authorizationResult = await _authorizationService.AuthorizeAsync(HttpContext.User, order, "OrderOwnerPolicy");
            if (authorizationResult.Succeeded)
            {
                return Ok(order);
            }
            else
            {
                throw CustomException.ForbiddenException("Only User or Admin has permission to access this property");
            }
        }


        [Authorize(Roles = "User")]
        [HttpPost()]
        public async Task<ActionResult<OrderReadDTO>> CreateOrderAsync([FromBody] OrderCreateDTO orderCreateDTO)
        {
            if (orderCreateDTO == null)
            {
                throw CustomException.BadRequestException("Invalid order data");
            }

            var userClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userClaim?.Value, out var userId))
            {
                throw CustomException.BadRequestException("Invalid user ID format");
            }

            if (userId != orderCreateDTO.UserId)
            {
                throw CustomException.ForbiddenException("Order owner mismatch with the logged-in customer");
            }

            var result = await _orderService.CreateOrderAsync(orderCreateDTO);
            return Ok(result);
        }



        [Authorize]
        [HttpDelete("{orderId}")]
        public async Task<ActionResult<bool>> DeleteOrderAsync(Guid orderId)
        {

            var order = await _orderService.GetOneOrderAsync(orderId) ?? throw CustomException.NotFoundException("No order is found with this id");

            if (order.OrderStatus != OrderStatus.CANCELED)
            {
                throw CustomException.ForbiddenException("Order must be confirmed as canceled before delete it");
            }
            var authorizationResult = await _authorizationService.AuthorizeAsync(HttpContext.User, order, "OrderAdminOrOwnerPolicy");
            if (authorizationResult.Succeeded)
            {
                var result = await _orderService.DeleteOrderAsync(orderId);
                return StatusCode(204, result);
            }
            else if (User.Identity.IsAuthenticated)
            {
                return new ForbidResult();
            }
            else
            {
                return new ChallengeResult();
            }
        }


        [Authorize(Roles = "User")]
        [HttpGet("getAllUserOrder/{userId}")]
        public async Task<ActionResult<IEnumerable<OneUserAllOrderReadDTO>>> GetOneUserAllOrdersAsync(Guid userId)
        {
            var result = await _orderService.GetOneUserAllOrdersAsync(userId);
            return Ok(result);
        }


        [Authorize(Roles = "Admin")]
        [HttpPatch("{orderId}")]
        public async Task<ActionResult<OrderReadDTO>> UpdateOrderAsync(Guid orderId, [FromBody] OrderUpdateDTO orderUpdateDTO)
        {
            var result = await _orderService.UpdateOrderAsync(orderId, orderUpdateDTO);
            return Ok(result);
        }


        [Authorize(Roles = "User")]
        [HttpPatch("{orderId}/payment")]
        public async Task<ActionResult<OrderReadDTO>> PaymentOrderAsync(Guid orderId)
        {

            var order = await _orderService.GetOneOrderAsync(orderId) ?? throw CustomException.NotFoundException("No order Found");

            if (order.OrderStatus != OrderStatus.PENDING)
            {
                throw CustomException.ForbiddenException("Order is already Paid or canceled");
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(HttpContext.User, order, "OrderOwnerPolicy");
            if (authorizationResult.Succeeded)
            {
                var result = await _orderService.UpdateOrderAsync(orderId, new OrderUpdateDTO { OrderStatus = OrderStatus.PAID });
                return Ok(result);
            }
            else
            {
                throw CustomException.ForbiddenException("Only User has permission to access this property");
            }

        }


        [Authorize(Roles = "User")]
        [HttpPatch("{orderId}/cancel")]
        public async Task<ActionResult<OrderReadDTO>> CancelOrderAsync(Guid orderId)
        {

            var order = await _orderService.GetOneOrderAsync(orderId) ?? throw CustomException.NotFoundException("No order Found");

            if (DateTime.UtcNow - order.CreatedDate > TimeSpan.FromDays(14))
            {
                throw CustomException.ForbiddenException("Order cannot be canceled as it is older than 14 days");
            }

            if (order.OrderStatus == OrderStatus.CANCELED)
            {
                throw CustomException.ForbiddenException("Order is already canceled");
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(HttpContext.User, order, "OrderOwnerPolicy");
            if (authorizationResult.Succeeded)
            {
                var result = await _orderService.UpdateOrderAsync(orderId, new OrderUpdateDTO { OrderStatus = OrderStatus.CANCELED });
                return Ok(result);
            }
            else
            {
                throw CustomException.ForbiddenException("Only User has permission to access this property");
            }

        }






    }
}