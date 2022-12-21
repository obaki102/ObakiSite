﻿using ObakiSite.Shared.DTO;
using ObakiSite.Shared.DTO.Response;

namespace ObakiSite.Application.Features.Email.Services
{
    public interface IEmailService
    {
        public Task<ApplicationResponse> SendEmail(EmailMessageDTO emailMessageDto);
    }
}
