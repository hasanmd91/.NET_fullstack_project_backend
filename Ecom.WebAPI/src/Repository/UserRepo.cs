using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
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

        public IEnumerable<User> GetAllUserAsync(GetAllParams options)
        {
            return _users.Include(u => u.Orders)
                                    .ThenInclude(od => od.OrderDetails)
                                    .ThenInclude(od => od.Product)
                                    .ThenInclude(od => od.Images)
                                    .Where(u => u.FirstName.Contains(options.Search)).Skip(options.Offset)
                                    .Take(options.Limit);
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
            var foundUser = await _users.FirstOrDefaultAsync(u => u.Email == email);
            return foundUser;
        }

        public async Task<User> UpdateOneUserAsync(Guid userId, User updatedUser)
        {
            var existingUser = await _users.FirstOrDefaultAsync(u => u.Id == userId);

            if (existingUser != null)
            {
                existingUser.FirstName = updatedUser.FirstName ?? existingUser.FirstName;
                existingUser.LastName = updatedUser.LastName ?? existingUser.LastName;
                existingUser.Email = updatedUser.Email ?? existingUser.Email;
                existingUser.Password = updatedUser.Password ?? existingUser.Password;
                existingUser.Avatar = updatedUser.Avatar ?? existingUser.Avatar;
                existingUser.Address = updatedUser.Address ?? existingUser.Address;
                existingUser.Zip = updatedUser.Zip ?? existingUser.Zip;
                existingUser.City = updatedUser.City ?? existingUser.City;

                _database.Update(existingUser);
                await _database.SaveChangesAsync();

            }

            return existingUser;
        }
    }
}
