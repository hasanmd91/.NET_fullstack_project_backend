using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;

namespace Ecom.Core.src.Abstraction
{
    public interface IUserRepo
    {
        Task<User> CreateOneUserAsync(User user);
        IEnumerable<User> GetAllUserAsync(GetAllParams options);
        Task<User> GetOneUserByIdAsync(Guid id);
        Task<User> UpdateOneUserAsync(Guid id, User user);
        Task<bool> DeleteOneUserAsync(Guid id);
        Task<User> GetOneUserByEmailAsync(string email);
    }
}
