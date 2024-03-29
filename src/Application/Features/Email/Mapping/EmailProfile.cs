﻿using AutoMapper;
using ObakiSite.Application.Domain.Entities;
using ObakiSite.Application.Shared.DTO;

namespace ObakiSite.Application.Features.Email.Mapping
{
    public class EmailProfile : AutoMapper.Profile
    {
        public EmailProfile()
        {
            CreateMap<EmailMessageDTO, EmailMessage>();
        }
    }
}
