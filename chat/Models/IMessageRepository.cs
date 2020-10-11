using System.Collections.Generic;

namespace chat.Models
{
    public interface IMessageRepository
    {
        Message GetMessage(string Id);
        IEnumerable<Message> GetAllMessages();
        Message Add(Message message);
        Message Update(Message message);
        Message Delete(string Id);
    }
}
