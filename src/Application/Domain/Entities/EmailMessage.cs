namespace ObakiSite.Application.Domain.Entities
{
    public class EmailMessage
    {
        public required string SenderName { get; set; } 
        public required string SenderEmail { get; set; } 
        public required string RecipientName { get; set; }
        public required string RecipientEmail { get; set; } 
        public required string Subject { get; set; }
        public string AttachmentFileName { get; set; } = string.Empty;
        public string AttachmentFilePath { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
