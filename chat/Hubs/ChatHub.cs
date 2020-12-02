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

            await Clients.Group(chatId).SendAsync("ReceiveMessage", user, message, newmessage.When.ToString("ddd MMM hh:mm")); 

            await Clients.Group(chatId).SendAsync("ReceiveMessageToChat", user, message, newmessage.When.ToString("h:m"), chatId);
        }
        public async Task AddToGroupAsync(string chatId)
        {
            if(chatId != "")
                await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }
    }
}
