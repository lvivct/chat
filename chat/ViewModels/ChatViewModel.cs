using chat.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace chat.ViewModels
{
    public class ChatViewModel
    {
        public string ChatName { get; set; }
        [Required]
        public string MessageText { get; set; }
        public string ChatId { get; set; }
        public string Sender { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
