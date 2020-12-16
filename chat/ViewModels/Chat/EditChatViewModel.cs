using Microsoft.AspNetCore.Http;

namespace chat.ViewModels.Chat
{
    public class EditChatViewModel
    {
        public string CurrentChatId { get; set; }
        public string CurrentChatName { get; set; }
        public IFormFile NewChatPhoto { get; set; }
    }
}
