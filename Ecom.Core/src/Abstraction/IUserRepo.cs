using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;

namespace Ecom.Core.src.Abstraction
{
    public interface IUserRepo
    {
        User CreateOne(User user);
        IEnumerable<User> GetAll(GetAllParams options);
        User GetOne(Guid id);
        User UpdateOne(User user);
        bool DeleteOne(Guid id);
    }
}
