using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.Enum;
using Ecom.Core.src.parameters;
using Ecom.Service.src.Shared;
using Ecom.WebAPI.src.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Org.BouncyCastle.Tls;

namespace Ecom.WebAPI.src.Repository
{
    public class OrderRepo : IOrderRepo
    {
        private readonly DbSet<Order> _orders;
        private readonly DbSet<User> _users;
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
                order.OrderStatus = OrderStatus.PENDING;

                foreach (var OrderDetail in order.OrderDetails)
                {
                    var product = await _database.Product.FindAsync(OrderDetail.ProductId)
                    ?? throw CustomException.TransactionException("Product is out of stock");
                    if (product.Quantity < OrderDetail.Quantity)
                    {
                        throw CustomException.TransactionException("Not enough products in inventory");
                    }
                    product.Quantity -= OrderDetail.Quantity;
                    _database.Product.Update(product);
                }

                await _orders.AddAsync(order);
                await _database.SaveChangesAsync();
                await transaction.CommitAsync();
                return order;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                await transaction.RollbackAsync();
                throw CustomException.BadRequestException();
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrderAsync(GetAllParams options)
        {
            var result = await _orders.Include(o => o.User)
                                    .Include(o => o.OrderDetails)
                                    .ThenInclude(od => od.Product)
                                    .ThenInclude(od => od.Images)
                                    .Skip(options.Offset).Take(options.Limit).ToListAsync();
            return result;
        }


        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            var order = await _orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order is not null)
            {
                _orders.Remove(order);
                await _database.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Order> GetOneOrderAsync(Guid orderId)
        {
            var result = await _orders.Include(o => o.User)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .ThenInclude(od => od.Images)
            .FirstOrDefaultAsync(o => o.Id == orderId);
            return result;
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            _orders.Update(order);
            await _database.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<Order>?> GetOneUserAllOrdersAsync(Guid userId)
        {

            var user = await _users.FirstOrDefaultAsync((u) => u.Id == userId);

            Console.WriteLine(user?.FirstName);

            if (user is null)
            {
                return null;
            }

            var orders = await _orders
                .Where(o => o.UserId == user.Id)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .ToListAsync();

            return orders;
        }
    }
}