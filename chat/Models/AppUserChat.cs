using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace chat.Models
{
    public class AppUserChat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }
        public AppUser User { get; set; }
        public string UserId { get; set; }
        public Chat Chat { get; set; }
        public string ChatId { get; set; }

       
       
    }
}
