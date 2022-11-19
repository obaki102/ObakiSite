using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using ObakiSite.Shared.Events;
using ObakiSite.Shared.Constants;
using ObakiSite.Shared.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;

namespace ObakiSite.Application.Features.Chat.Services
{
    public class ChatHubClient : IChatHubClient, IAsyncDisposable
    {
        private HubConnection? hubConnection;
        private readonly ChatHubClientOptions _hubClientOptions;
        private bool isConnectionStarted = false;
        public string HubUrl => _hubClientOptions.HubUrl;

        public string HubConenctionId => hubConnection?.ConnectionId ?? string.Empty;

        public ChatHubClient(IOptions<ChatHubClientOptions> hubClientOptions)
        {
            if (hubClientOptions == null)
            {
                throw new ArgumentNullException(nameof(hubClientOptions));
            }

            _hubClientOptions = hubClientOptions.Value;
        }

        public async Task ConnectAsync()
        {
            if (!isConnectionStarted)
            {
                hubConnection = new HubConnectionBuilder()
                               .WithUrl(_hubClientOptions.HubUrl)
                                .AddMessagePackProtocol()
                                .Build();

                hubConnection.On<string>(HubHandler.ReceivedMessage, (receivedMessage) =>
                {
                    var chatMessage = JsonConvert.DeserializeObject<ChatMessage>(receivedMessage);
                    ReceivedMessageHandler?.Invoke(this, new ChatMessageEventArgs { ChatMessage = chatMessage ?? new() });
                });

                await hubConnection.StartAsync();
                isConnectionStarted = true;
            }
        }

        public async Task DisconnectAsync()
        {
            if (isConnectionStarted && hubConnection is not null)
            {
                await hubConnection.StopAsync();
                await hubConnection.DisposeAsync();
                hubConnection = null;
                isConnectionStarted = false;
            }
        }

        public event EventHandler<ChatMessageEventArgs>? ReceivedMessageHandler;

        public async ValueTask DisposeAsync()
        {
            await DisconnectAsync();
        }
    }
}
