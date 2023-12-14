using Ecom.Core.src.Abstraction;
using Ecom.Core.src.parameters;
using Ecom.Service.src.Shared;

namespace Ecom.Service.src.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepo _userRepo;
        public AuthService(IUserRepo userRepo)
        {
            _userRepo = userRepo;

        }

        public string Login(Credentials credentials)
        {
            var foundUserByEmail = _userRepo.GetOneByEmail(credentials.Email) ?? throw CustomException.NotFoundException();
            var isPasswordMatch = PasswordService.VerifyPassword(credentials.Password, foundUserByEmail.Password, foundUserByEmail.Salt);
            if (!isPasswordMatch)
            {
                throw CustomException.UnauthorizedException("Incorrect password");
            }

            throw new NotImplementedException();

        }
    }
}