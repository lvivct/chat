using chat.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace chat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext AppDb;
        public ChatHub(AppDbContext appDb)
        {
            AppDb = appDb;
        }       
        public async Task SendMessage(string user, string message, string chatId)
        {
            Message newmessage = new Message
            {
                ChatId = chatId,
                SenderName = user,
                MessageText = message
            };
            await AppDb.MessagesDatabase.AddAsync(newmessage);
            await AppDb.SaveChangesAsync();
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
