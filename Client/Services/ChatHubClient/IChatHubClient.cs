using ObakiSite.Shared.Events;

namespace ObakiSite.Client.Services.ChatHubClient
{
    public interface IChatHubClient
    {
        Task ConnectAsync();
        Task DisconnectAsync();

        event EventHandler<ChatMessageEventArgs>? ReceivedMessageHandler;
    }
}
