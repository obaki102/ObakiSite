using ObakiSite.Shared.Events;

namespace ObakiSite.Application.Features.Chat.Services
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
