using chat.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace chat.ViewModels
{
    public class ChatEditViewModel
    {
        public string ChatId { get; set; }

        [Required]
        public string ChatName { get; set; }
        public string ForPhoto { get; set; }
        public string NewUserName { get; set; }
        public List<string> UserNameList { get; set; }
        public List<string> UserIdList { get; set; }
        public IFormFile Photo { get; set; }
        public string PhotoPath { get; set; }
        public bool EditChat { get; set; }
        public bool KickUsers { get; set; }
        public bool GiveRoles { get; set; }
        public bool AddUsersToChat { get; set; }
    }
}
