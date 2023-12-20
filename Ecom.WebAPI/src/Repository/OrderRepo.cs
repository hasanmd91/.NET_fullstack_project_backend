using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
using Ecom.Service.src.Shared;
using Ecom.WebAPI.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecom.WebAPI.src.Repository
{
    public class OrderRepo : IOrderRepo
    {
        private readonly DbSet<Order> _orders;
        private readonly DbSet<User> _users;
        private DbSet<Product> _products;
        private readonly DataBaseContext _database;
        public OrderRepo(DataBaseContext dataBase)
        {
            _orders = dataBase.Order;
            _users = dataBase.Users;
            _database = dataBase;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            using var transaction = await _database.Database.BeginTransactionAsync();
            try
            {

                order.User = await _users.FindAsync(order.UserId);

                //some bug while finding order.OrderDetails
                // foreach (var OrderDetail in order.OrderDetails)
                // {
                //     var product = await _products.FindAsync(OrderDetail.ProductId) ?? throw CustomException.TransactionException("Product is out of stock");
                //     if (product.Quantity < OrderDetail.Quantity)
                //     {
                //         throw CustomException.TransactionException("Not enough products in inventory");
                //     }
                //     product.Quantity -= OrderDetail.Quantity;
                //     _products.Update(product);
                // }

                await _orders.AddAsync(order);
                await _database.SaveChangesAsync();
                await transaction.CommitAsync();
                return order;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrderAsync(GetAllParams options)
        {
            var result = await _orders.Include(o => o.User).Include(o => o.OrderDetails).Skip(options.Offset).Take(options.Limit).ToListAsync();
            return result;

        }
    }
}