using Ecom.Service.src.DTO;

namespace Ecom.Service.src.Abstraction
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);

    }
}