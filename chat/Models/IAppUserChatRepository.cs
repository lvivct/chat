using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat.Models
{
    public interface IAppUserChatRepository
    {
        AppUserChat GetAppUser(string Id);
        IEnumerable<AppUserChat> GetAllAppUsersChats();
        AppUserChat Add(AppUserChat chat);
        AppUserChat Update(AppUserChat chat);
        AppUserChat Delete(string ID);
    }
}
