using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
using Ecom.WebAPI.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecom.WebAPI.src.Repository
{
    public class UserRepo : IUserRepo
    {
        private DbSet<User> _users;
        private DataBaseContext _database;
        private IConfiguration _config;

        public UserRepo(DataBaseContext dataBase, IConfiguration config)
        {
            _users = dataBase.Users;
            _config = config;
            _database = dataBase;
        }
        public User CreateOne(User user)
        {
            _users.Add(user);
            _database.SaveChanges();
            return user;
        }

        public bool DeleteOne(Guid userId)
        {
            var user = _users.FirstOrDefault(u => u.id == userId);
            _users.Remove(user);
            return true;

        }

        public IEnumerable<User> GetAll(GetAllParams options)
        {
            return _users.Where(u => u.FirstName.Contains(options.Search)).Skip(options.Offset).Take(options.Limit);
        }

        public User GetOne(Guid userId)
        {
            var user = _users.FirstOrDefault(u => u.id == userId);
            return user;
        }

        public User UpdateOne(Guid id, User user)
        {
            throw new NotImplementedException();
        }
    }

}