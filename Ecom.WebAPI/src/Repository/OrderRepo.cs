using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
using Ecom.WebAPI.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecom.WebAPI.src.Repository
{
    public class OrderRepo : IOrderRepo
    {
        private readonly DbSet<Order> _orders;
        private readonly DbSet<Product> _products;
        private readonly DbSet<OrderDetails> _orderDetails;
        private readonly DataBaseContext _database;
        public OrderRepo(DataBaseContext dataBase)
        {
            _orders = dataBase.Order;
            _orderDetails = dataBase.OrderDetails;
            _products = dataBase.Product;
            _database = dataBase;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {

            await _orders.AddAsync(order);
            await _database.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrderAsync(GetAllParams options)
        {
            var result = await _orders.Include(o => o.User).Include(o => o.OrderDetails).Include(o => o.OrderDetails).ToListAsync();
            return result;

        }
    }
}