using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
using Ecom.Service.src.DTO;

namespace Ecom.Service.src.Abstraction
{
    public interface IOrderService
    {
        public Task<Order> CreateOrderAsync(Order Order);
        public Task<IEnumerable<Order>> GetAllOrderAsync(GetAllParams options);

    }
}