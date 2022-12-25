using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.Events;

namespace ObakiSite.Application.Features.Chat.Services
{
    public interface IChatHubClient
    {
        Task ConnectAsync();
        ValueTask DisposeAsync();
        string HubConenctionId { get; }
        Task SendMessage(ChatMessage chatMessage);
        event EventHandler<ChatMessageEventArgs>? OnReceivedMessage;
        event EventHandler<bool>? OnConnecting;
        event EventHandler<bool>? OnConnected;
        event EventHandler<ClosedConnectionEventArgs>? OnClosed;
        event EventHandler<string>? OnConnectionError;
    }
}
