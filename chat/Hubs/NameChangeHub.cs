using chat.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace chat.Hubs
{
    public class NameChangeHub : Hub
    {
        private readonly AppDbContext AppDb;
        private readonly UserManager<AppUser> _userManager;
        public NameChangeHub(AppDbContext appDb
            , UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            AppDb = appDb;
        }

        public async Task UserChange(string userId, string userName, string email)
        {
            var CurrentUser = AppDb.Find<AppUser>(userId);
            if (CurrentUser.Email != email || CurrentUser.UserName != userName)
            {
                if (CurrentUser.Email != email)
                    CurrentUser.Email = email;

                bool name = false;
                if (CurrentUser.UserName != userName)
                {
                    CurrentUser.UserName = userName;

                    var messages = AppDb.MessagesDatabase;
                    foreach (var message in messages)
                        if (message.SenderId == userId)
                            message.SenderName = userName;
                    name = true;
                }

                await _userManager.UpdateAsync(CurrentUser);
                await AppDb.SaveChangesAsync();

                if (name)
                    await Clients.All.SendAsync("ReceiveMessage", userId, userName);
            }
        }
    }
}
