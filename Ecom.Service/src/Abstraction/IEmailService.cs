using Ecom.Core.src.Entity;
using Ecom.Service.src.DTO;

namespace Ecom.Service.src.Abstraction
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailDto request);
        Task SendOrderConfirmationEmailAsync(Order order);
    }
}