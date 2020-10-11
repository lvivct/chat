using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using chat.Models;
using chat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace chat.Controllers
{
    public class ChatController : Controller
    {
        private readonly AppDbContext AppDb;
        private readonly IChatRepository ChatsRepository;
        private readonly IMessageRepository MessagesRepository;

        public ChatController(IChatRepository chatsRepository, IMessageRepository messagesRepository
            , AppDbContext appDb)
        {
            MessagesRepository = messagesRepository;
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

        [HttpGet]
        public IActionResult Open(string chatId)
        {
            var CurrentUserName = User.FindFirst(ClaimTypes.Name).Value;
            var CurrentChat = AppDb.ChatsDatabase.Include("Messages")
                .ToList().Find(e => e.ChatId == chatId);

            ChatViewModel NewView = new ChatViewModel
            { 
                Sender = CurrentUserName,
                ChatId = CurrentChat.ChatId,
                ChatName = CurrentChat.ChatName,
                Messages = CurrentChat.Messages
            };
            return View(NewView);
        }
        [HttpPost]
        public IActionResult Open(string chatId, string messageText)
        {
            var CurrentUserName = User.FindFirst(ClaimTypes.Name).Value;
            var CurrentChat = AppDb.ChatsDatabase.Include("Messages")
               .ToList().Find(e => e.ChatId == chatId);
            if (ModelState.IsValid)
            {
                Message newmessage = new Message
                {
                    MessageText = messageText,
                    SenderName = CurrentUserName,
                    ChatId = CurrentChat.ChatId
                };
                MessagesRepository.Add(newmessage);
            }
            ChatViewModel NewView = new ChatViewModel
            {
                Sender = CurrentUserName,
                ChatId = CurrentChat.ChatId,
                ChatName = CurrentChat.ChatName,
                Messages = CurrentChat.Messages
            };
            return RedirectToAction("Open", "Chat", new { chatId = chatId });
        }
    }
}
