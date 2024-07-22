using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.DTO.Email;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BadmintonCenter.Service.Interface;
using BadmintonCenter.Common.DTO.User;

namespace BadmintonCenter.Service
{
    public class EmailService: IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendInfomationModeratorEmail(string userEmail, string subject, CreateUserDTO info)
        {
            var sendEmail = _configuration.GetSection("SendEmailAccount")["Email"];
            var toEmail = userEmail;
            var htmlBody = EmailTemplate.ModeratorInfoTemplate(userEmail, subject, info.Email, info.FullName, info.PhoneNumber);
            MailMessage mailMessage = new MailMessage(sendEmail, toEmail, subject, htmlBody);
            mailMessage.IsBodyHtml = true;

            var smtpServer = _configuration.GetSection("SendEmailAccount")["SmtpServer"];
            int.TryParse(_configuration.GetSection("SendEmailAccount")["Port"], out int port);
            var userNameEmail = _configuration.GetSection("SendEmailAccount")["UserName"];
            var password = _configuration.GetSection("SendEmailAccount")["Password"];

            SmtpClient client = new SmtpClient(smtpServer, port);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(userNameEmail, password);
            client.EnableSsl = true; // Enable SSL/TLS encryption

            client.Send(mailMessage);
        }
    }
}
