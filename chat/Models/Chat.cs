using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chat.Models
{
    public class Chat
    {
        public Chat()
        {
            PhotoPath = "no_chat_avatar.jpg";
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string ChatId { get; set; }
        [Required]
        public string ChatName { get; set; }
        public string PhotoPath { get; set; }
        public ICollection<AppUserChat> AppUsersChats { get; set; }
        public ICollection<Message> Messages { get; set; }

        public string LastMessageText { get; set; } // temp here
        public string LastMessageSender { get; set; }
        public DateTime LastMessageWhen { get; set; } //
    }
}
