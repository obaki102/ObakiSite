using AutoMapper;
using ObakiSite.Application.Domain.Entities;
using ObakiSite.Shared.DTO;

namespace ObakiSite.Application.Features.Email.Mapping
{
    public class EmailProfile : Profile
    {
        public EmailProfile()
        {
            CreateMap<EmailMessageDTO, EmailMessage>();
        }
    }
}
