using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;

namespace Ecom.Core.src.Abstraction
{
    public interface IOrderRepo
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<IEnumerable<Order>> GetAllOrderAsync(GetAllParams options);
        Task<bool> DeleteOrderAsync(Guid orderId);
        Task<Order> GetOneOrderAsync(Guid orderId);
        Task<Order> UpdateOrderAsync(Order order);

    }
}