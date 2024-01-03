using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.Enum;
using Ecom.Core.src.parameters;
using Ecom.WebAPI.src.Database;
using Microsoft.EntityFrameworkCore;


namespace Ecom.WebAPI.src.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly DbSet<User> _users;
        private readonly DataBaseContext _database;

        public UserRepo(DataBaseContext dataBase)
        {
            _users = dataBase.Users;
            _database = dataBase;
        }

        public async Task<User> CreateOneUserAsync(User user)
        {
            _users.Add(user);
            await _database.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteOneUserAsync(Guid userId)
        {
            var user = await _users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                _users.Remove(user);
                await _database.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<User>> GetAllUserAsync(GetAllParams options)
        {
            var users = await _users
                .Include(u => u.Orders)
                .Where(u => u.FirstName.Contains(options.Search))
                .OrderBy(u => u.FirstName)
                .Skip(options.Offset)
                .Take(options.Limit)
                .ToListAsync();

            return users;
        }




        public async Task<User> GetOneUserByIdAsync(Guid userId)
        {
            var user = await _users.Include(u => u.Orders)
                                    .ThenInclude(od => od.OrderDetails)
                                    .ThenInclude(od => od.Product)
                                    .ThenInclude(od => od.Images)
                                    .FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }

        public async Task<User> GetOneUserByEmailAsync(string email)
        {
            var foundUser = await _users.Include(u => u.Orders)
                               .ThenInclude(od => od.OrderDetails)
                               .ThenInclude(od => od.Product)
                               .ThenInclude(od => od.Images).
                               FirstOrDefaultAsync(u => u.Email == email);
            return foundUser;
        }

        public async Task<User> UpdateOneUserAsync(User updatedUser)
        {
            _database.Update(updatedUser);
            await _database.SaveChangesAsync();
            return updatedUser;
        }
    }
}
