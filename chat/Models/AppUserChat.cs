using System;
using System.Diagnostics.CodeAnalysis;


namespace chat.Models
{
    public class AppUserChat
    { 
        public AppUser User { get; set; }
        public string UserId { get; set; }
        public Chat Chat { get; set; }
        public string ChatId { get; set; }

// temp here!!!
        private void ctor(string str) 
        {
            RoleName = str;
            KickUsers = false;
            GiveRoles = false;
            EditChat = false;
            AddUsersToChat = false;
        }
        public AppUserChat()
        {
            ctor("default");
        }
        public AppUserChat(string str)
        {
            if (str == "admin")
            {
                RoleName = "admin";
                KickUsers = true;
                GiveRoles = true;
                EditChat = true;
                AddUsersToChat = true;
            }
            else
                ctor(str);
        }
        public string RoleName { get; set; }
        public bool GiveRoles { get; set; }//1
        public bool KickUsers { get; set; }//2
        public bool EditChat { get; set; } //3
        public bool AddUsersToChat { get; set; } //4 
//
    }
}
