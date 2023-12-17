using Ecom.Core.src.Abstraction;
using Ecom.Core.src.parameters;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.Shared;

namespace Ecom.Service.src.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepo _userRepo;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepo userRepo, ITokenService tokenService)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
        }

        public async Task<string> Login(Credentials credentials)
        {
            var foundUserByEmail = await _userRepo.GetOneUserByEmailAsync(credentials.Email) ?? throw CustomException.NotFoundException();
            var isPasswordMatch = PasswordService.VerifyPassword(credentials.Password, foundUserByEmail.Password, foundUserByEmail.Salt);
            if (!isPasswordMatch)
            {
                throw CustomException.UnauthorizedException("Incorrect password");
            }
            return _tokenService.GenerateToken(foundUserByEmail);
        }
    }
}