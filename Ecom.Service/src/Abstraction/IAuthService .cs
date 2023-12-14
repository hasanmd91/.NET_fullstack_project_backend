using Ecom.Core.src.parameters;

namespace Ecom.Service.src.Service
{
    public interface IAuthService
    {
        string Login(Credentials credentials);
    }
}