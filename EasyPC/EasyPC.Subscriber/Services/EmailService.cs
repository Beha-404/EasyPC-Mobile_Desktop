using System.Net;
using System.Net.Mail;
using System.Text;
using EasyPC.Model.Messages;
using Microsoft.Extensions.Configuration;

namespace EasyPC.Subscriber.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendOrderConfirmationEmail(OrderEmailMessage orderMessage)
        {
            var smtpServer = _configuration["Email:SmtpServer"] 
                ?? Environment.GetEnvironmentVariable("SMTP_SERVER") 
                ?? "smtp.gmail.com";
                
            var smtpPort = int.Parse(_configuration["Email:SmtpPort"] 
                ?? Environment.GetEnvironmentVariable("SMTP_PORT") 
                ?? "587");
                
            var senderEmail = _configuration["Email:SenderEmail"] 
                ?? Environment.GetEnvironmentVariable("SMTP_USERNAME") 
                ?? "noreply@easypc.com";
                
            var senderName = _configuration["Email:SenderName"] 
                ?? "EasyPC Support";
                
            var senderPassword = _configuration["Email:SenderPassword"] 
                ?? Environment.GetEnvironmentVariable("SMTP_PASSWORD") 
                ?? throw new InvalidOperationException("SMTP password not configured");
                
            var enableSsl = bool.Parse(_configuration["Email:EnableSsl"] 
                ?? Environment.GetEnvironmentVariable("SMTP_ENABLE_SSL") 
                ?? "true");

            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail, senderName),
                Subject = $"Order Confirmation #{orderMessage.OrderId} - EasyPC",
                Body = GenerateEmailBody(orderMessage),
                IsBodyHtml = true
            };

            mailMessage.To.Add(orderMessage.UserEmail);

            using var smtpClient = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = enableSsl
            };

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] ✓ Email sent successfully to {orderMessage.UserEmail} for Order #{orderMessage.OrderId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] ✗ Failed to send email: {ex.Message}");
                throw;
            }
        }

        private string GenerateEmailBody(OrderEmailMessage order)
        {
            var itemsHtml = new StringBuilder();
            foreach (var item in order.OrderItems)
            {
                itemsHtml.Append($@"
                <tr>
                    <td style='padding: 10px; border-bottom: 1px solid #eee;'>{item.PcName}</td>
                    <td style='padding: 10px; border-bottom: 1px solid #eee; text-align: center;'>{item.Quantity}</td>
                    <td style='padding: 10px; border-bottom: 1px solid #eee; text-align: right;'>${item.UnitPrice:F2}</td>
                    <td style='padding: 10px; border-bottom: 1px solid #eee; text-align: right;'>${item.TotalPrice:F2}</td>
                </tr>");
            }

            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
</head>
<body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
    <div style='max-width: 600px; margin: 0 auto; padding: 20px; background-color: #f9f9f9;'>
        <div style='background-color: #1F1F1F; color: #DDC03D; padding: 20px; text-align: center;'>
            <h1 style='margin: 0;'>🖥️ EasyPC</h1>
            <p style='margin: 5px 0 0 0; font-size: 14px;'>Custom PC Builder</p>
        </div>

        <div style='background-color: white; padding: 30px; margin-top: 20px; border-radius: 5px;'>
            <h2 style='color: #DDC03D; margin-top: 0;'>Order Confirmation</h2>
            <p>Hi <strong>{order.UserName}</strong>,</p>
            <p>Thank you for your order! Your custom PC configuration has been confirmed.</p>

            <div style='background-color: #f5f5f5; padding: 15px; margin: 20px 0; border-left: 4px solid #DDC03D;'>
                <p style='margin: 5px 0;'><strong>Order Number:</strong> #{order.OrderId}</p>
                <p style='margin: 5px 0;'><strong>Order Date:</strong> {order.OrderDate:MMMM dd, yyyy HH:mm}</p>
                <p style='margin: 5px 0;'><strong>Payment Method:</strong> {order.PaymentMethod}</p>
            </div>

            <h3 style='color: #1F1F1F; border-bottom: 2px solid #DDC03D; padding-bottom: 10px;'>Order Details</h3>
            <table style='width: 100%; border-collapse: collapse; margin: 20px 0;'>
                <thead>
                    <tr style='background-color: #1F1F1F; color: #DDC03D;'>
                        <th style='padding: 12px; text-align: left;'>Product</th>
                        <th style='padding: 12px; text-align: center;'>Qty</th>
                        <th style='padding: 12px; text-align: right;'>Price</th>
                        <th style='padding: 12px; text-align: right;'>Total</th>
                    </tr>
                </thead>
                <tbody>
                    {itemsHtml}
                </tbody>
                <tfoot>
                    <tr style='background-color: #f9f9f9;'>
                        <td colspan='3' style='padding: 15px; text-align: right; font-weight: bold; font-size: 18px;'>Total Amount:</td>
                        <td style='padding: 15px; text-align: right; font-weight: bold; font-size: 18px; color: #DDC03D;'>${order.TotalPrice:F2}</td>
                    </tr>
                </tfoot>
            </table>

            <p style='margin-top: 30px;'>Your order is being processed and will be shipped soon. You will receive a tracking number once your order ships.</p>

            <p>If you have any questions, feel free to contact our support team.</p>

            <p>Best regards,<br><strong>EasyPC Team</strong></p>
        </div>

        <div style='text-align: center; padding: 20px; color: #666; font-size: 12px;'>
            <p>&copy; {DateTime.Now.Year} EasyPC. All rights reserved.</p>
            <p>This is an automated message, please do not reply to this email.</p>
        </div>
    </div>
</body>
</html>";
        }
    }
}
