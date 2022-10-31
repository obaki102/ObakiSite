using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using ObakiSite.Shared.Events;
using ObakiSite.Shared.Constants;
using ObakiSite.Shared.Models;
using Newtonsoft.Json;

namespace ObakiSite.Client.Services.ChatHubClient
{
    public class ChatHubClient : IChatHubClient, IAsyncDisposable
    {
        private HubConnection? hubConnection;
        private readonly ChatHubClientOptions _hubClientOptions;
        private bool isConnectionStarted = false;

        public ChatHubClient(IOptions<ChatHubClientOptions> hubClientOptions)
        {
            _hubClientOptions = hubClientOptions.Value;
        }

        public async Task ConnectAsync()
        {
            if (!isConnectionStarted)
            {
                hubConnection = new HubConnectionBuilder()
                               .WithUrl(_hubClientOptions.HubUrl)
                                .ConfigureLogging(logging =>
                                {
                                    logging.ClearProviders();
                                })
                                     .AddMessagePackProtocol()
                                     .Build();


                hubConnection.On<object>(HubHandler.ReceivedMessage, (receivedMessage) =>
                {
                    var json = JsonConvert.SerializeObject(receivedMessage);
                    var chatMessage = JsonConvert.DeserializeObject<ChatMessage>(json);
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
