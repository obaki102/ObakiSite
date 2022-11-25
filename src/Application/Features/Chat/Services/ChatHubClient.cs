using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using ObakiSite.Shared.Events;
using ObakiSite.Shared.Constants;
using ObakiSite.Shared.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace ObakiSite.Application.Features.Chat.Services
{
    public class ChatHubClient : IChatHubClient, IAsyncDisposable
    {
        private readonly ChatHubClientOptions _hubClientOptions;
        private readonly HttpClient _httpClient;
        private HubConnection? hubConnection;
        private bool isConnectionStarted = false;
        private string HubUrl => _hubClientOptions.HubUrl;
        public string HubConenctionId => hubConnection?.ConnectionId ?? string.Empty;

        public ChatHubClient(IOptions<ChatHubClientOptions> hubClientOptions, IHttpClientFactory httpClientFactory)
        {
            if (hubClientOptions == null)
            {
                throw new ArgumentNullException(nameof(hubClientOptions));
            }
            _hubClientOptions = hubClientOptions.Value;
            _httpClient = httpClientFactory.CreateClient(HttpNameClient.Default);

        }

        public async Task ConnectAsync()
        {
            if (!isConnectionStarted)
            {
                hubConnection = new HubConnectionBuilder()
                               .WithUrl(_hubClientOptions.HubUrl)
                                .AddMessagePackProtocol()
                                .WithAutomaticReconnect()
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

        public async Task SendMessage(ChatMessage chatMessage)
        {
            await _httpClient.PostAsJsonAsync($"{HubUrl}/messages", chatMessage);
        }
    }
}
