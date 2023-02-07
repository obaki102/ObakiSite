namespace ObakiSite.Application.Features.Chat.Services
{
    public class ChatHubClientOptions
    {
        /// <summary>
        /// Create a new instance of HubClientOptions with its Default settings. 
        /// </summary>
        public static ChatHubClientOptions Default => new ChatHubClientOptions();
        public string HubUrl { get; set; } = string.Empty;
        public bool AddMessagePackProtocol { get; set; } = true;
        public bool LoggingEnabled { get; set; } = true;

    }
}
