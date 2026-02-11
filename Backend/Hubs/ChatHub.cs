using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using OnlineChat.Models;

namespace OnlineChat.Hubs
{
    public interface IChatInterface
    {
        public Task RecieveMessage(string user, string message);
    }
    public class ChatHub : Hub<IChatInterface>
    {
        private readonly IDistributedCache _cache;
        public ChatHub(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task JoinChat(ChatConnection connection)
        {
            var stringConnection = JsonSerializer.Serialize(connection);

            await _cache.SetStringAsync(Context.ConnectionId, stringConnection);
            
            await Groups.AddToGroupAsync(Context.ConnectionId, connection.chatName);
            
            await Clients
                .Group(connection.chatName)
                .RecieveMessage("Admin", $"{connection.userName} зашел в чат.");
        }
        public async Task SendMessage(string message)
        {
            var stringConnection = await _cache.GetStringAsync(Context.ConnectionId);
            
            var connection = JsonSerializer.Deserialize<ChatConnection>(stringConnection);

            if (connection is not null)
                await Clients
                    .Group(connection.chatName)
                    .RecieveMessage(connection.userName, message);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var stringConnection = await _cache.GetStringAsync(Context.ConnectionId);
            
            var connection = JsonSerializer.Deserialize<ChatConnection>(stringConnection);

            if (connection is not null)
            {
                await _cache.RemoveAsync(Context.ConnectionId);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.chatName);
                await Clients
                    .Group(connection.chatName)
                    .RecieveMessage("Admin", $"{connection.userName} вышел из чата.");
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}