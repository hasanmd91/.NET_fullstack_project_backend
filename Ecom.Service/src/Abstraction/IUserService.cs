using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
using Ecom.Service.src.DTO;

namespace Ecom.Service.src.Abstraction
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDTO>> GetAllUserAsync(GetAllParams options);
        Task<UserReadDTO> GetOneUserByIdAsync(Guid id);
        Task<UserReadDTO> CreateOneUserAsync(UserCreateDTO userCreateDTO);
        Task<UserReadDTO> UpdateOneUserAsync(Guid userId, UserUpdateDTO userUpdateDTO);
        Task<bool> DeleteOneUserAsync(Guid id);

    }
}