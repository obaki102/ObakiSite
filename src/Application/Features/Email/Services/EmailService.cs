using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using ObakiSite.Application.Shared.Constants;
using Microsoft.Extensions.Options;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Features.Email.Constants;
using ObakiSite.Application.Domain.Entities;

namespace ObakiSite.Application.Features.Email.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailServiceOptions _emailServiceOptions;
        private readonly IHttpClientFactory _httpClientFactory;
        public EmailService(IOptions<EmailServiceOptions> emailServiceOptions, 
            IHttpClientFactory httpClientFactory)
        {
            _emailServiceOptions = emailServiceOptions.Value;
            _httpClientFactory = httpClientFactory;
        }

      
        public async Task<bool> SendEmail(EmailMessageDTO emailMessageDto)
        {
            try
            {
                if(emailMessageDto is null)
                       throw new ArgumentNullException(nameof(emailMessageDto));

                EmailMessage emailMessage = emailMessageDto;
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

                //todo: check markupstring or explore a more elegant way to format the html body.Save email template to storage.
                string htmlBody = """"
                                <p> Hello,<br><br>
                                Thank you so much for taking interest in my profile, please refer to the attachment for my latest CV..<br>
                                Have a pleasent day.<br><br>
                                Regards,<br>
                                Josh</p>
                                """";

                builder.HtmlBody = htmlBody;
                message.Body = builder.ToMessageBody();


                using (var emailClient = new SmtpClient())
                {
                    await emailClient.ConnectAsync(EmailConstants.SmtpServer, 587, SecureSocketOptions.Auto).ConfigureAwait(false);
                    emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                    await emailClient.AuthenticateAsync(EmailConstants.DefaultEmail, _emailServiceOptions.AppPassword).ConfigureAwait(false);
                    await emailClient.SendAsync(message).ConfigureAwait(false);
                    await emailClient.DisconnectAsync(true).ConfigureAwait(false);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<Stream> GetFile(string filePath)
        {
            //todo: store the file stream in db instead.
            var httpClient = _httpClientFactory.CreateClient(HttpNameClientConstants.Email);
            var result = await httpClient.GetAsync(filePath).ConfigureAwait(false);

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStreamAsync().ConfigureAwait(false);
                return content;
            }

            return Stream.Null;
        }
    }
}
