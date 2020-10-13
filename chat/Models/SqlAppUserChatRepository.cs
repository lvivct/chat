using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace chat.Models
{
    public class SqlAppUserChatRepository : IAppUserChatRepository
    {
        private readonly AppDbContext context;
        public SqlAppUserChatRepository(AppDbContext context)
        {
            this.context = context;
        }
        public AppUserChat Add(AppUserChat appUser)
        {
            context.ChatsUsersDatabase.Add(appUser);
            context.SaveChanges();
            return appUser;
        }
        public AppUserChat Delete(string Id)
        {
            AppUserChat appUser = context.ChatsUsersDatabase.Find(Id);
            if (appUser != null)
            {
                context.ChatsUsersDatabase.Remove(appUser);
                context.SaveChanges();
            }
            return appUser;
        }
        public IEnumerable<AppUserChat> GetAllAppUsersChats()
        {
            return context.ChatsUsersDatabase;
        }
        public AppUserChat GetAppUserChat(string Id)
        {
            return context.ChatsUsersDatabase.Find(Id);
        }
        public AppUserChat Update(AppUserChat appUser)
        {
            var _appUser = context.ChatsUsersDatabase.Attach(appUser);
            _appUser.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return appUser;
        }
    }
}
