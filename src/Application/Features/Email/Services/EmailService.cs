using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using ObakiSite.Shared.Models;
using MailKit.Net.Smtp;
using ObakiSite.Shared.Constants;
using ObakiSite.Shared.Models.Response;
using Microsoft.Extensions.Options;
using ObakiSite.Application.Features.Email.Commands;

namespace ObakiSite.Application.Features.Email.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailServiceOptions _emailServiceOptions;
        public EmailService(IOptions<EmailServiceOptions> emailServiceOptions)
        {
            _emailServiceOptions = emailServiceOptions.Value;
        }
        public EmailService(EmailServiceOptions emailServiceOptions)
        {
            _emailServiceOptions = emailServiceOptions;
        }
        public Task<ApplicationResponse> SendEmail(SendEmail emailMessage)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(emailMessage.SenderName, emailMessage.SenderEmail));
            message.To.Add(new MailboxAddress(emailMessage.RecipientName, emailMessage.RecipientEmail));
            message.Subject = emailMessage.Subject;

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = emailMessage.Message
            };

            try
            {
                using (var emailClient = new SmtpClient())
                {
                    emailClient.Connect(EmailConstants.SmtpServer, 587, SecureSocketOptions.Auto);
                    emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                    emailClient.Authenticate(EmailConstants.DefaultEmail, _emailServiceOptions.AppPassword);
                    emailClient.Send(message);
                    emailClient.Disconnect(true);
                }

                return Task.FromResult(ApplicationResponse.Success());
            }
            catch (Exception ex)
            {
                return Task.FromResult(ApplicationResponse.Fail(ex.Message));
            }
        }
    }
}
