namespace ObakiSite.Shared.Models
{
    public class EmailMessage
    {
        public string SenderName { get; set; } = "Joshua J L. Piluden";
        public string SenderEmail { get; set; } = "joshuajpiluden@gmail.com";
        public string RecipientName { get; set; } = string.Empty;
        public string RecipientEmail { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string AttachmentFilePath { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
