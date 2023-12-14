using Ecom.Core.src.Entity;

namespace Ecom.Service.src.Abstraction
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}