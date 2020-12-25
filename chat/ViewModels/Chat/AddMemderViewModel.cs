using System.ComponentModel.DataAnnotations;

namespace chat.ViewModels.Chat
{
    public class AddMemderViewModel
    {
        public string CurrentChatId { get; set; }
        [Required]
        public string NewUserName { get; set; }
    }
}
