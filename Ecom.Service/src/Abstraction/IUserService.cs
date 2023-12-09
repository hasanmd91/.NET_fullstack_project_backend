using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
using Ecom.Service.src.DTO;

namespace Ecom.Service.src.Abstraction
{
    public interface IUserService
    {
        IEnumerable<UserReadDTO> GetAll(GetAllParams options);
        UserReadDTO GetOne(Guid id);
        UserReadDTO CreateOne(UserCreateDTO userCreateDTO);
        UserReadDTO UpdateOne(UserUpdateDTO userUpdateDTO);
        bool DeleteOne(Guid id);

    }
}