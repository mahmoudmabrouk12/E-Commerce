using E_Commerce.Core.DTOs;
using E_Commerce.Core.Services;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace E_Commerce.InfraStructure.Repository.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmail(EmailDTO emailDTO)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("E-Commerce", configuration["EmailSetting:From"]));
            message.To.Add(new MailboxAddress(emailDTO.To, emailDTO.To));
            message.Subject = emailDTO.Subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html) 
            {
               Text = emailDTO.Content
            };

            using (var Smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    await Smtp.ConnectAsync
                        (
                        configuration["EmailSetting:Smtp"],
                        int.Parse(configuration["EmailSetting:Port"]),
                        true
                        );
                    await Smtp.AuthenticateAsync
                        (
                        configuration["EmailSetting:Username"],
                        configuration["EmailSetting:Password"]
                        );
                    await Smtp.SendAsync(message);
                }
                catch (Exception Ex)
                {

                    throw;
                }
                finally 
                {
                    Smtp.Disconnect(true);
                    Smtp.Dispose();
                }
            
            }

        }
    }
}
