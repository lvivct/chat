using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace chat.Models
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string MessageId { get; set; }
        [Required]
        public string MessageText { get; set; }
        public string SenderName { get; set; }
        public DateTime When { get; set; }
        public Chat Chat { get; set; }
        public string ChatId { get; set; }
    }
}
