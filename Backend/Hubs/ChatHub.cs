using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using OnlineChat.Models;

namespace OnlineChat.Hubs
{
    public interface IChatInterface
    {
        public Task RecieveMessage(string user, string message);

    }
    public class ChatHub : Hub<IChatInterface>
    {
        public async Task JoinChat(ChatConnection connection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, connection.chatName);
            await Clients
                .Group(connection.chatName)
                .RecieveMessage("Admin", $"User {connection.userName} joined to the chat.");
        }
        public async Task SendMessage(ChatConnection connection, string message)
        {
            await Clients
                .Group(connection.chatName)
                .RecieveMessage(connection.userName, $"User {connection.chatName} says: {message}.");
        }
    }
}