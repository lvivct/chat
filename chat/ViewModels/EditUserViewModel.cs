using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace chat.ViewModels
{
    public class EditUserViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string UserId { get; set; }
        public IFormFile Photo { get; set; }
        public string PhotoPath { get; set; }
    }
}
