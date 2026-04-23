using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CRMSarritelApi.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message, string type = "P2P")
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message, type);
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessageToGroup(string groupName, string user, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveGroupMessage", groupName, user, message);
        }
    }
}
