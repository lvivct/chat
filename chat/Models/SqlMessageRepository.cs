using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat.Models
{
    public class SqlMessageRepository : IMessageRepository
    {
        private readonly AppDbContext context;

        public SqlMessageRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Message Add(Message message)
        {
            context.MessagesDatabase.Add(message);
            context.SaveChanges();
            return message;
        }
        public Message Delete(string Id)
        {
            Message chat = context.MessagesDatabase.Find(Id);
            if (chat != null)
            {
                context.MessagesDatabase.Remove(chat);
                context.SaveChanges();
            }
            return chat;
        }
        public IEnumerable<Message> GetAllMessages()
        {
            return context.MessagesDatabase;
        }
        public Message GetMessage(string Id)
        {
            return context.MessagesDatabase.Find(Id);
        }
        public Message Update(Message message)
        {
            var _message = context.MessagesDatabase.Attach(message);
            _message.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return message;
        }
    }
}
