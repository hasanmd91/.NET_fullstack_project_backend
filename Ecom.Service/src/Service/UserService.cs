using AutoMapper;
using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.Enum;
using Ecom.Core.src.parameters;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.DTO;
using Ecom.Service.src.Shared;

namespace Ecom.Service.src.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }
        public async Task<UserReadDTO> CreateOneUserAsync(UserCreateDTO userCreateDTO)
        {
            PasswordService.HashPassword(userCreateDTO.Password, out string hashedPassword, out byte[] salt);

            var user = _mapper.Map<UserCreateDTO, User>(userCreateDTO);
            user.Password = hashedPassword;
            user.Salt = salt;

            var result = await _userRepo.CreateOneUserAsync(user) ?? throw CustomException.BadRequestException();
            return _mapper.Map<User, UserReadDTO>(result);
        }

        public async Task<bool> DeleteOneUserAsync(Guid id)
        {
            var result = await _userRepo.DeleteOneUserAsync(id);

            if (!result)
            {
                throw CustomException.NotFoundException();
            }

            return result;
        }

        public async Task<IEnumerable<UserReadDTO>> GetAllUserAsync(GetAllParams options)
        {
            var result = _userRepo.GetAllUserAsync(options);
            var users = await Task.FromResult(result);
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserReadDTO>>(users);
        }

        public async Task<UserReadDTO> GetOneUserByIdAsync(Guid id)
        {
            var result = await _userRepo.GetOneUserByIdAsync(id) ?? throw CustomException.NotFoundException();
            return _mapper.Map<User, UserReadDTO>(result);
        }

        public async Task<UserReadDTO> UpdateOneUserAsync(Guid userId, UserUpdateDTO userUpdateDTO)
        {
            var userToUpdate = await _userRepo.GetOneUserByIdAsync(userId) ?? throw CustomException.NotFoundException();

            var updateUser = _mapper.Map(userUpdateDTO, userToUpdate);

            var result = await _userRepo.UpdateOneUserAsync(updateUser);
            return _mapper.Map<User, UserReadDTO>(result);
        }


        public async Task<UserReadDTO> ChangeUserRoleAsync(Guid userid)
        {

            User foundUser = await _userRepo.GetOneUserByIdAsync(userid) ?? throw CustomException.NotFoundException("User not found");

            if (foundUser.Role == Role.Admin)
            {
                throw CustomException.BadRequestException("User is already an Admin");
            }

            ChangeRoleDTO admin = new();
            var newAdminUser = _mapper.Map(admin, foundUser);
            var result = await _userRepo.UpdateOneUserAsync(newAdminUser);

            return _mapper.Map<User, UserReadDTO>(result);

        }

    }
}
