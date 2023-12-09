using AutoMapper;
using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.DTO;

namespace Ecom.Service.src.Service
{
    public class UserService : IUserService
    {
        private IUserRepo _userRepo;
        private IMapper _mapper;
        public UserService(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;

        }

        public UserReadDTO CreateOne(UserCreateDTO userCreateDTO)
        {
            var result = _userRepo.CreateOne(_mapper.Map<UserCreateDTO, User>(userCreateDTO));
            return _mapper.Map<User, UserReadDTO>(result);
        }

        public bool DeleteOne(Guid id)
        {
            return _userRepo.DeleteOne(id);
        }

        public IEnumerable<UserReadDTO> GetAll(GetAllParams options)
        {
            var result = _userRepo.GetAll(options);
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserReadDTO>>(result);

        }

        public UserReadDTO GetOne(Guid id)
        {
            var result = _userRepo.GetOne(id);
            return _mapper.Map<User, UserReadDTO>(result);

        }

        public UserReadDTO UpdateOne(UserUpdateDTO userUpdateDTO)
        {
            var result = _userRepo.UpdateOne(_mapper.Map<UserUpdateDTO, User>(userUpdateDTO));
            return _mapper.Map<User, UserReadDTO>(result);

        }
    }
}