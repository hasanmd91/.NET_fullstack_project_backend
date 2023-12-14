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

            PasswordService.HashPassword(userCreateDTO.Password, out string hashedPassword, out byte[] salt);

            var user = _mapper.Map<UserCreateDTO, User>(userCreateDTO);
            user.Password = hashedPassword;
            user.Salt = salt;

            var result = _userRepo.CreateOne(user) ?? throw CustomException.BadRequestException();
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

        public UserReadDTO UpdateOne(Guid userId, UserUpdateDTO userUpdateDTO)
        {
            var result = _userRepo.UpdateOne(userId, _mapper.Map<UserUpdateDTO, User>(userUpdateDTO)) ?? throw CustomException.NotFoundException();
            return _mapper.Map<User, UserReadDTO>(result);

        }
    }
}