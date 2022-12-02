using MailKit.Security;
using MimeKit;
using ObakiSite.Shared.Models;
using MailKit.Net.Smtp;
using ObakiSite.Shared.Constants;
using ObakiSite.Shared.Models.Response;
using Microsoft.Extensions.Options;

namespace ObakiSite.Application.Features.Email.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailServiceOptions _emailServiceOptions;
        private readonly IHttpClientFactory _httpClientFactory;
        public EmailService(IOptions<EmailServiceOptions> emailServiceOptions, IHttpClientFactory httpClientFactory)
        {
            _emailServiceOptions = emailServiceOptions.Value;
            _httpClientFactory = httpClientFactory;
        }

        public EmailService(EmailServiceOptions emailServiceOptions, IHttpClientFactory httpClientFactory)
        {
            _emailServiceOptions = emailServiceOptions;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApplicationResponse> SendEmail(EmailMessage emailMessage)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(emailMessage.SenderName, emailMessage.SenderEmail));
            message.To.Add(new MailboxAddress(emailMessage.RecipientName, emailMessage.RecipientEmail));
            message.Subject = emailMessage.Subject;

            var builder = new BodyBuilder();
            builder.TextBody = emailMessage.Message;
            var fileStream = await GetFile(emailMessage.AttachmentFilePath);
            if (fileStream.Length > 0)
            {
                builder.Attachments.Add(emailMessage.AttachmentFileName, fileStream);
            }
            message.Body = builder.ToMessageBody();

            try
            {
                using (var emailClient = new SmtpClient())
                {
                    await emailClient.ConnectAsync(EmailConstants.SmtpServer, 587, SecureSocketOptions.Auto);
                    emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                    await emailClient.AuthenticateAsync(EmailConstants.DefaultEmail, _emailServiceOptions.AppPassword);
                    await emailClient.SendAsync(message);
                    await emailClient.DisconnectAsync(true);
                }

                return ApplicationResponse.Success();
            }
            catch (Exception ex)
            {
                return ApplicationResponse.Fail(ex.Message);
            }
        }
        private async Task<Stream> GetFile(string filePath)
        {
            var httpClient = _httpClientFactory.CreateClient(HttpNameClient.Email);
            var result =  await httpClient.GetAsync(filePath);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStreamAsync();
                return content;
            }
            return Stream.Null;
        }
    }
}
