using ObakiSite.Application.Shared;
using ObakiSite.Application.Shared.DTO;

namespace ObakiSite.Application.Features.Email.Services
{
    public interface IEmailService
    {
        public Task<bool> SendEmail(EmailMessageDTO emailMessageDto);
    }
}
