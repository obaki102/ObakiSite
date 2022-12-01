using ObakiSite.Application.Features.Chat.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObakiSite.Application.Features.Email.Services
{
    public class EmailServiceOptions
    {
        public static EmailServiceOptions Default => new EmailServiceOptions();
        public string AppPassword { get; set; } = String.Empty;
      
    }
}
