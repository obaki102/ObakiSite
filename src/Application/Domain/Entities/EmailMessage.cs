using ObakiSite.Application.Domain.Primitives;

namespace ObakiSite.Application.Domain.Entities
{
    public class EmailMessage : Entity
    {
        public EmailMessage(Guid id, string senderName, string senderEmail, string recipientName,
            string recipientEmail, string subject, string attachmentFileName, string attachmentFilePath,
            string message
            ) : base(id)
        {
            SenderName = senderName;
            SenderEmail = senderEmail;
            RecipientName = recipientName;
            RecipientEmail = recipientEmail;
            Subject = subject;
            AttachmentFileName = attachmentFileName;
            AttachmentFilePath = attachmentFilePath;
            Message = message;
        }
        public string SenderName { get; init; }
        public string SenderEmail { get; init; }
        public string RecipientName { get; init; }
        public string RecipientEmail { get; init; }
        public string Subject { get; init; }
        public string AttachmentFileName { get; init; } = string.Empty;
        public string AttachmentFilePath { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;
    }
}
