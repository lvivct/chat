using Microsoft.AspNetCore.Identity;
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
        public AppUser User { get; set; }
        public string UserId { get; set; }
    }
}
