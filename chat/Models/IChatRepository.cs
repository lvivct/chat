using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace chat.Models
{
    public interface IChatRepository
    {
        Chat GetChat(string Id);
        IEnumerable<Chat> GetAllChats();
        Chat Add(Chat chat);
        Chat Update(Chat chat);
        Chat Delete(string ID);
    }
}
