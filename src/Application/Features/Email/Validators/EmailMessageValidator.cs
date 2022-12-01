using FluentValidation;
using ObakiSite.Application.Features.Email.Commands;

namespace ObakiSite.Application.Features.Email.Validators
{
    public class EmailMessageValidator : AbstractValidator<SendEmail>
    {
        public EmailMessageValidator()
        {
            RuleFor(x => x.EmailMessage.RecipientEmail)
             .NotEmpty().WithMessage("Your recipient email cannot be empty.")
             .EmailAddress().WithMessage("A valid email is required.")
             .Length(2, 100);
        }
    }
}
