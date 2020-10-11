using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace chat.Models
{
    public class SqlChatRepository : IChatRepository
    {
        private readonly AppDbContext context;

        public SqlChatRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Chat Add(Chat chat)
        {
            context.ChatsDatabase.Add(chat);
            context.SaveChanges();
            return chat;
        }
        public Chat Delete(string Id)
        {
            Chat chat = context.ChatsDatabase.Find(Id);
            if (chat != null)
            {
                context.ChatsDatabase.Remove(chat);
                context.SaveChanges();
            }
            return chat;
        }
        public IEnumerable<Chat> GetAllChats()
        {
            return context.ChatsDatabase;
        }
        public Chat GetChat(string Id)
        {
            return context.ChatsDatabase.Find(Id);
        }
        public Chat Update(Chat chat)
        {
            var _chat = context.ChatsDatabase.Attach(chat);
            _chat.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return chat;
        }
    }
}
