using chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat.ViewModels
{
    public class AddUserViewModel
    {
        public  string ChatId { get; set; }
        public string UserName { get; set; }
        public bool AddUsersToChat { get; set; }
    }
}
