﻿namespace ObakiSite.Application.Features.Email.Services
{
    public sealed class EmailServiceOptions
    {
        public static EmailServiceOptions Default => new EmailServiceOptions();
        public string AppPassword { get; set; } = String.Empty;
      
    }
}
