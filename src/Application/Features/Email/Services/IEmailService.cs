using ObakiSite.Application.Features.Email.Commands;
using ObakiSite.Shared.Models;
using ObakiSite.Shared.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObakiSite.Application.Features.Email.Services
{
    public interface IEmailService
    {
        public Task<ApplicationResponse> SendEmail(SendEmail emailMessage);
    }
}
