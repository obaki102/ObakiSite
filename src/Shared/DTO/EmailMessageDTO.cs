namespace ObakiSite.Shared.DTO
{
    public record EmailMessageDTO
    {
        public string SenderName { get; init; } = "Joshua J L. Piluden";
        public string SenderEmail { get; init; } = "joshuajpiluden@gmail.com";
        public string RecipientName { get; init; } = string.Empty;
        public string RecipientEmail { get; init; } = string.Empty;
        public string Subject { get; init; } = "Joshua J L. Piluden CV";
        public string AttachmentFileName { get; init; } = string.Empty;
        public string AttachmentFilePath { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;
    }
}
