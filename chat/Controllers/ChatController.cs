using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using chat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace chat.Controllers
{
    public class ChatController : Controller
    {
        private readonly AppDbContext AppDb;
        private readonly IChatRepository ChatsRepository;

        public ChatController(IChatRepository chatsRepository, AppDbContext appDb)
        {
            ChatsRepository = chatsRepository;
            AppDb = appDb;
        }

        public IActionResult Chats()
        {
            var CurrentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var CurrentUser = AppDb.Users.Include("Chats").ToList().Find(e => e.Id == CurrentUserId);
            return View(CurrentUser.Chats);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Chat model)
        {
            if (ModelState.IsValid)
            {
                var CurrentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Chat newchat = new Chat
                {
                    ChatName = model.ChatName,
                    UserId = CurrentUser
                };
                ChatsRepository.Add(newchat);
                return RedirectToAction("Chats", "Chat");
            }
            return View();
        }
        public IActionResult Open(string chatId)
        {
            var CurrentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var CurrentUser = AppDb.Users.Include("Chats").ToList().Find(e => e.Id == CurrentUserId);
            var CurrentChat = CurrentUser.Chats.ToList().Find(e => e.ChatId == chatId);
            return View(CurrentChat);
        }
    }
}
