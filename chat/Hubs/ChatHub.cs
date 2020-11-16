using chat.Models;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
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
        public async Task SendMessage(string user, string message, string chatId, string userId)
        {
            Message newmessage = new Message
            {
                ChatId = chatId,
                SenderId = userId,
                SenderName = user,
                MessageText = message
            };
            var currentChat = await AppDb.ChatsDatabase.FindAsync(chatId);
            currentChat.LastMessageSender = user + ": ";
            currentChat.LastMessageText = message;
            currentChat.LastMessageWhen = newmessage.When;

            await AppDb.MessagesDatabase.AddAsync(newmessage);
            await AppDb.SaveChangesAsync();

            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
            await Clients.Group(chatId).SendAsync("ReceiveMessage", user, message); 

            await Clients.Group(chatId).SendAsync("ReceiveMessageToChat", user, message, newmessage.When, chatId);
         
        }
    }
}
