using ObakiSite.Application.Domain.Entities;

namespace ObakiSite.Application.Shared.DTO
{
    public record EmailMessageDTO
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string SenderName { get; init; } = "Joshua J L. Piluden";
        public string SenderEmail { get; init; } = "joshuajpiluden@gmail.com";
        public string RecipientName { get; init; } = string.Empty;
        public string RecipientEmail { get; init; } = string.Empty;
        public string Subject { get; init; } = "Joshua J L. Piluden CV";
        public string AttachmentFileName { get; init; } = string.Empty;
        public string AttachmentFilePath { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;

        public static implicit operator EmailMessage(EmailMessageDTO emailMessageDTO)
        {
            return new EmailMessage(
                emailMessageDTO.Id,
                emailMessageDTO.SenderEmail,
                emailMessageDTO.SenderEmail,
                emailMessageDTO.RecipientName,
                emailMessageDTO.RecipientEmail,
                emailMessageDTO.Subject,
                emailMessageDTO.AttachmentFileName,
                emailMessageDTO.AttachmentFilePath,
                emailMessageDTO.Message
                );
        }

        public static explicit operator EmailMessageDTO(EmailMessage emailMessage)
        {
            return new EmailMessageDTO
            {
                Id = emailMessage.Id,
                AttachmentFileName = emailMessage.AttachmentFileName,
                SenderEmail = emailMessage.SenderEmail,
                RecipientName = emailMessage.RecipientName,
                RecipientEmail = emailMessage.RecipientEmail,
                Subject = emailMessage.Subject,
                AttachmentFilePath = emailMessage.AttachmentFilePath,
                Message = emailMessage.Message
            };
        }
    }
}
