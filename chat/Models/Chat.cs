﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chat.Models
{
    public class Chat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string ChatId { get; set; }
        [Required]
        public string ChatName { get; set; }
        public string PhotoPath { get; set; }
        public ICollection<AppUserChat> AppUsersChats { get; set; }
        public ICollection<Message> Messages { get; set; }

    }
}