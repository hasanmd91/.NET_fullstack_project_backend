using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;

namespace Ecom.Core.src.Abstraction
{
    public interface IOrderRepo
    {
        public Task<Order> CreateOrderAsync(Order order);
        public Task<IEnumerable<Order>> GetAllOrderAsync(GetAllParams options);

    }
}