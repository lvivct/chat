using chat.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace chat.ViewModels
{
    public class AllChatsViewModel
    {
        public List<AppUserChat> ChatList { get; set; }
        public List<Message> MessageList { get; set; }
        public string CurrentChatId { get; set; }
        public string CurrentChatName { get; set; }
        public string CurrentUserId { get; set; }
        public string CurrentUserPhotoPath { get; set; }
        public string CurrentChatPhotoPath { get; set; }


        public string NewMessageText { get; set; }
        public string NewUserName { get; set; }
        public IFormFile NewChatPhoto { get; set; }


        public List<string> UserNameList { get; set; }
        public List<string> UserIdList { get; set; }


        public bool EditChat { get; set; }
        public bool KickUsers { get; set; }
        public bool GiveRoles { get; set; }
        public bool AddUsersToChat { get; set; }
    }
}
