using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using ObakiSite.Shared.Constants;
using ObakiSite.Shared.DTO.Response;
using Microsoft.Extensions.Options;
using ObakiSite.Shared.DTO;

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
            
            string htmlBody = """"
                                <p> Hello,<br><br>
                                Thank you so much for taking interest in my profile, please don’t hesitate to contact me if I you need additional information.<br>
                                Have a pleasent day.<br><br>
                                Regards,<br>
                                Josh</p>
                                """";
           
            builder.HtmlBody = htmlBody;
            message.Body = builder.ToMessageBody();
            
            try
            {
                using (var emailClient = new SmtpClient())
                {
                    await emailClient.ConnectAsync(EmailConstants.SmtpServer, 587, SecureSocketOptions.Auto).ConfigureAwait(false);
                    emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                    await emailClient.AuthenticateAsync(EmailConstants.DefaultEmail, _emailServiceOptions.AppPassword).ConfigureAwait(false);
                    await emailClient.SendAsync(message).ConfigureAwait(false);
                    await emailClient.DisconnectAsync(true).ConfigureAwait(false);
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
            var result =  await httpClient.GetAsync(filePath).ConfigureAwait(false);
           
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStreamAsync().ConfigureAwait(false) ;
                return content;
            }
           
            return Stream.Null;
        }
    }
}
