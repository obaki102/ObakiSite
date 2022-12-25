using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.DTO.Response;
using Microsoft.Extensions.Options;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Features.Email.Constants;
using ObakiSite.Application.Domain.Entities;
using AutoMapper;

namespace ObakiSite.Application.Features.Email.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailServiceOptions _emailServiceOptions;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        public EmailService(IOptions<EmailServiceOptions> emailServiceOptions, 
            IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _emailServiceOptions = emailServiceOptions.Value;
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }

        public EmailService(EmailServiceOptions emailServiceOptions, IHttpClientFactory httpClientFactory)
        {
            _emailServiceOptions = emailServiceOptions;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApplicationResponse> SendEmail(EmailMessageDTO emailMessageDto)
        {
            try
            {

                var emailMessage = _mapper.Map<EmailMessage>(emailMessageDto);
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

                //toddo: check markupstring or explore a more elegant way to format the html body
                string htmlBody = """"
                                <p> Hello,<br><br>
                                Thank you so much for taking interest in my profile, please don’t hesitate to contact me if I you need additional information.<br>
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

                return ApplicationResponse.Success();
            }
            catch (Exception ex)
            {
                return ApplicationResponse.Fail(ex.Message);
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
