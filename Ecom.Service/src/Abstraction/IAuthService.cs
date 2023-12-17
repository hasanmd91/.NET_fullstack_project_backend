using Ecom.Core.src.parameters;

namespace Ecom.Service.src.Service
{
    public interface IAuthService
    {
        Task<string> Login(Credentials credentials);
    }
}