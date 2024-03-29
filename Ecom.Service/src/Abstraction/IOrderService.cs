using Ecom.Core.src.parameters;
using Ecom.Service.src.DTO;

namespace Ecom.Service.src.Abstraction
{
    public interface IOrderService
    {
        public Task<OrderReadDTO> CreateOrderAsync(OrderCreateDTO orderCreateDTO);
        public Task<IEnumerable<OrderReadDTO>> GetAllOrderAsync(GetAllParams options);
        public Task<bool> DeleteOrderAsync(Guid orderId);
        public Task<OrderReadDTO> GetOneOrderAsync(Guid orderId);
        public Task<OrderReadDTO> UpdateOrderAsync(Guid orderId, OrderUpdateDTO orderUpdateDTO);
        public Task<IEnumerable<OneUserAllOrderReadDTO>> GetOneUserAllOrdersAsync(Guid id);
    }
}