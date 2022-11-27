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
                try
                {
                    OnConnecting?.Invoke(this, true);
                    hubConnection = new HubConnectionBuilder()
                                   .WithUrl(_hubClientOptions.HubUrl)
                                    .AddMessagePackProtocol()
                                    .WithAutomaticReconnect()
                                    .Build();
                    HubConnectionState state = hubConnection.State;
                    bool isConnected = state.Equals(HubConnectionState.Connected);
                    hubConnection.Closed += (exception) =>
                    {
                        return Task.Run(() =>
                         {
                             if (exception == null)
                             {
                                 OnClosed?.Invoke(this, new ClosedConnectionEventArgs(true, "Connection closed sucessfully."));
                             }
                             else
                             {
                                 OnClosed?.Invoke(this, new ClosedConnectionEventArgs(true, $"Connection closed due to an error: {exception}"));
                             }
                         });
                    };
                    //hubConnection.Reconnecting += (exception) =>
                    //{
                    //    return Task.Run(() =>
                    //    {
                    //        //todo: Create  reconnecting event. 
                    //    });
                    //};
                    //hubConnection.Reconnected += (connectionId) =>
                    //{
                    //    return Task.Run(() =>
                    //    {
                    //        //todo: Create  reconencted event. 
                    //    });
                    //};
                    hubConnection.On<string>(HubHandler.ReceivedMessage, (receivedMessage) =>
                    {
                        var chatMessage = JsonConvert.DeserializeObject<ChatMessage>(receivedMessage);
                        OnReceivedMessage?.Invoke(this, new ChatMessageEventArgs { ChatMessage = chatMessage ?? new() });
                    });

                    await hubConnection.StartAsync();
                    OnConnected?.Invoke(this, hubConnection.State.Equals(HubConnectionState.Connected) ? true : false);
                    isConnectionStarted = true;
                }
                catch (HttpRequestException ex)
                {
                    OnConnectionError?.Invoke(this, ex.Message);
                }
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

        public event EventHandler<ChatMessageEventArgs>? OnReceivedMessage;
        public event EventHandler<bool>? OnConnecting;
        public event EventHandler<bool>? OnConnected;
        public event EventHandler<ClosedConnectionEventArgs>? OnClosed;
        public event EventHandler<string>? OnConnectionError;

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
