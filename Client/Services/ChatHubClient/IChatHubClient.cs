using ObakiSite.Shared.Events;

namespace ObakiSite.Client.Services.ChatHubClient
{
    public interface IChatHubClient
    {
        Task ConnectAsync();
        Task DisconnectAsync();
        string HubUrl { get; }
        string HubConenctionId { get; }

        event EventHandler<ChatMessageEventArgs>? ReceivedMessageHandler;
    }
}
