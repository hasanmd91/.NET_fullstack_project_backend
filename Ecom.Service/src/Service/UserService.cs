using AutoMapper;
using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.DTO;
using Ecom.Service.src.Shared;

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
            var result = _userRepo.CreateOne(_mapper.Map<UserCreateDTO, User>(userCreateDTO)) ?? throw CustomException.NotFoundException();
            return _mapper.Map<User, UserReadDTO>(result);
        }

        public bool DeleteOneById(Guid id)
        {
            var result = _userRepo.DeleteOneById(id);

            if (!result)
            {
                throw CustomException.NotFoundException();
            }

            return result;

        }

        public IEnumerable<UserReadDTO> GetAll(GetAllParams options)
        {
            var result = _userRepo.GetAll(options);
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserReadDTO>>(result);

        }

        public UserReadDTO GetOne(Guid id)
        {
            var result = _userRepo.GetOne(id) ?? throw CustomException.NotFoundException();
            return _mapper.Map<User, UserReadDTO>(result);

        }

        public UserReadDTO GetOneByEmail(LoginDTO loginDTO)
        {
            var result = _userRepo.GetOneByEmail(_mapper.Map<LoginDTO, User>(loginDTO)) ?? throw CustomException.UnauthorizedException();
            return _mapper.Map<User, UserReadDTO>(result);

        }

        public UserReadDTO UpdateOne(Guid userId, UserUpdateDTO userUpdateDTO)
        {
            var result = _userRepo.UpdateOne(userId, _mapper.Map<UserUpdateDTO, User>(userUpdateDTO)) ?? throw CustomException.NotFoundException();
            return _mapper.Map<User, UserReadDTO>(result);

        }
    }
}