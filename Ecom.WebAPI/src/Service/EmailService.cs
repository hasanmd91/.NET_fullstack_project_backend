using Ecom.Core.src.Entity;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.DTO;
using Ecom.WebAPI.src.Service;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Ecom.Service.src.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendOrderConfirmationEmailAsync(Order order)
        {
            var emailDto = ConvertOrderToEmailDTO(order);

            string emailBody = CreateOrderEmailTemplate(order);
            emailDto.Body = emailBody;
            await SendEmailAsync(emailDto);
        }


        private static EmailDto ConvertOrderToEmailDTO(Order order)
        {
            return new EmailDto
            {
                To = order.User.Email,
                Subject = "Order Confirmation",
                Body = string.Empty
            };
        }



        private static string CreateOrderEmailTemplate(Order order)
        {
            string emailTemplate = $"Dear {order.User.FirstName},<br><br>";
            emailTemplate += "Thank you for your order!<br><br>";
            emailTemplate += "Order Details:<br>";

            foreach (var orderDetail in order.OrderDetails)
            {
                emailTemplate += $"{orderDetail.Product.Title} - Quantity: {orderDetail.Quantity}<br>";
            }

            emailTemplate += $"<br>Total Price: {order.TotalPrice}<br><br>";
            emailTemplate += "Thank you for shopping with us!<br>";

            return emailTemplate;
        }

        public async Task SendEmailAsync(EmailDto request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }


    }

}