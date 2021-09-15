using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ChatBlazorApp.Server.Hubs
{
    public class ChatHub : Hub
    {
        private const string GroupName = "ChatRoom";

        public override async Task OnConnectedAsync()
        {
            await SendMessage("Somebody", "Joined");
            await Groups.AddToGroupAsync(Context.ConnectionId, GroupName);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await SendMessage("Somebody", "Left");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, GroupName);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.Group(GroupName).SendAsync("ReceiveMessage", user, message);
        }
    }
}
