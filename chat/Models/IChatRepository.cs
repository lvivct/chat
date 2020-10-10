using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace chat.Models
{
    public interface IChatRepository
    {
        Chat GetChats(int id);
        IEnumerable<Chat> GetAllChats();
        Chat Add(Chat guy);
        Chat Update(Chat guy);
        Chat Delete(int ID);
    }
}
