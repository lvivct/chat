using chat.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
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
        public async Task SendMessage(string user, string message, string chatId, string userId, string photoPath)
        {
            Message newmessage = new Message
            {
                ChatId = chatId,
                SenderId = userId,
                SenderName = user,
                MessageText = message,
                PhotoPath = photoPath
            };
            var currentChat = await AppDb.ChatsDatabase.FindAsync(chatId);
            currentChat.LastMessageSender = user + ": ";
            currentChat.LastMessageText = message;
            currentChat.LastMessageWhen = newmessage.When;

            await AppDb.MessagesDatabase.AddAsync(newmessage);
            await AppDb.SaveChangesAsync();

            await Clients.Group("Full " + chatId).SendAsync("ReceiveMessage", user, message, newmessage.When.ToString("ddd MMM hh:mm")); 

            await Clients.Group("Part " + chatId).SendAsync("ReceiveMessageToChat", user, message, newmessage.When.ToString("h:m"), chatId);
        }
        public async Task AddToGroupAsync(string userId, string chatId)
        {
            if (chatId != "")
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Full " + chatId);

                var AllUsers = AppDb.Users
                    .Include(x => x.AppUsersChats)
                    .ThenInclude(x => x.Chat);
                var CurrentUser = AllUsers.ToList().Find(e => e.Id == userId);
                var ChatList = CurrentUser.AppUsersChats;
                foreach (var item in ChatList)
                    await Groups.AddToGroupAsync(Context.ConnectionId, "Part " + item.ChatId);
            }
        }
    }
}
