using Microsoft.AspNetCore.SignalR;

namespace WebAPI.SignalR
{
    public class NotificationHub : Hub
    {
        public async Task Notify(int userId)
        {
            // Send the message to all connected clients
            await Clients.All.SendAsync("Notify", userId);
        }
    }
}
