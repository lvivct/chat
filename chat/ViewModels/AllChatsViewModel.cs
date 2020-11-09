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
        public string ChatName { get; set; }
    }
}
