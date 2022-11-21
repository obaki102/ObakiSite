using ObakiSite.Shared.Events;
using ObakiSite.Shared.Models;

namespace ObakiSite.Application.Features.Chat.Services
{
    public interface IChatHubClient
    {
        Task ConnectAsync();
        Task DisconnectAsync();
        string HubConenctionId { get; }
        Task SendMessage(ChatMessage chatMessage);

        event EventHandler<ChatMessageEventArgs>? ReceivedMessageHandler;
    }
}
