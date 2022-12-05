namespace ObakiSite.Shared.DTO
{
    public class ChatMessage
    {
        private DateTime _createDate;

        public ChatMessage()
        {
            _createDate = DateTime.Now;
        }

        public string User { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

        public DateTime MessageCreateDate { get => _createDate; }

    }
}