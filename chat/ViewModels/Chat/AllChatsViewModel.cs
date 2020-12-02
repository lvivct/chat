using chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat.ViewModels
{
    public class AllChatsViewModel
    {
        public List<AppUserChat> ChatList { get; set; }
        public List<Message> MessageList { get; set; }
        public string ChatName { get; set; }
        public string CurrentUserId { get; set; }
        public string CurrentUserPhotoPath { get; set; }
        public string CurrentChatId { get; set; }
        public string NewMessageText { get; set; }
    }
}
