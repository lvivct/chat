using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using chat.Models;
using chat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace chat.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly AppDbContext AppDb;
        private readonly IChatRepository ChatsRepository;
        private readonly IMessageRepository MessagesRepository;
        private readonly IAppUserChatRepository AppUserChatRepository;

        public ChatController(IChatRepository chatsRepository, IMessageRepository messagesRepository
            , IAppUserChatRepository appUserChatRepository, AppDbContext appDb)
        {
            MessagesRepository = messagesRepository;
            ChatsRepository = chatsRepository;
            AppUserChatRepository = appUserChatRepository;
            AppDb = appDb;
        }

        public IActionResult Chats()
        {
            var CurrentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var AllUsers = AppDb.Users
                        .Include(x => x.AppUsersChats)
                        .ThenInclude(x => x.Chat);

            var CurrentUser = AllUsers.ToList().Find(e => e.Id == CurrentUserId);
            var Chats = CurrentUser.AppUsersChats.ToList();
            return View(Chats);
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
                Chat newchat = new Chat
                {
                    ChatName = model.ChatName,
                };
                ChatsRepository.Add(newchat);

                var CurrentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                AppUserChat newAppUserChat = new AppUserChat
                {
                    ChatId = newchat.ChatId,
                    UserId = CurrentUser
                };
                AppUserChatRepository.Add(newAppUserChat);
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
                ChatId = CurrentChat.ChatId,
                ChatName = CurrentChat.ChatName,
                Messages = CurrentChat.Messages
            };
            return View(NewView);
        }

        [HttpPost]
        public IActionResult Open(string chatId, string messageText)
        {
            if(messageText == null)
                return RedirectToAction("Open", "Chat", new { chatId = chatId });

            var CurrentUserName = User.FindFirst(ClaimTypes.Name).Value;
            var CurrentChat = AppDb.ChatsDatabase.Include("Messages")
               .ToList().Find(e => e.ChatId == chatId);
            if (ModelState.IsValid)
            {
                Message newmessage = new Message
                {
                    MessageText = messageText,
                    SenderName = CurrentUserName,
                    ChatId = CurrentChat.ChatId,
                    When = DateTime.Now
                };
                newmessage.MessageId = MessagesRepository.GetAllMessages().Count() + "";
                MessagesRepository.Add(newmessage);
            }
            ChatViewModel NewView = new ChatViewModel
            {
                ChatId = CurrentChat.ChatId,
                ChatName = CurrentChat.ChatName,
                Messages = CurrentChat.Messages
            };
            return RedirectToAction("Open", "Chat", new { chatId = chatId });
        }


        [HttpGet]
        public IActionResult AddMemder(string chatId)
        {
            AddUserViewModel NewView = new AddUserViewModel
            {
                ChatId = chatId
            };
            return View(NewView);
        }
       [HttpPost]
        public IActionResult AddMemder(string chatId, string userName)
        {
            if (ModelState.IsValid)
            {
                var User = AppDb.Users
                    .Where(b => b.UserName == userName)
                    .FirstOrDefault();

                short exeption = 0;
                if (User == null)
                    exeption = 1;
                else
                {
                    AppUserChat newAppUserChat = new AppUserChat
                    {
                        ChatId = chatId,
                        UserId = User.Id
                    };
                    foreach (var x in AppUserChatRepository.GetAllAppUsersChats().ToList())
                        if (x.ChatId == newAppUserChat.ChatId && x.UserId == newAppUserChat.UserId)
                        {
                            exeption = 2;
                        }
                    if (exeption == 0)
                    {
                        AppUserChatRepository.Add(newAppUserChat);
                        return RedirectToAction("Chats", "Chat");
                    }
                }
                if (exeption == 1)
                    ModelState.AddModelError(string.Empty, "This User doesn't exist");
                else
                    ModelState.AddModelError(string.Empty, "This User already added");
                AddUserViewModel NewView = new AddUserViewModel
                {
                    ChatId = chatId
                };
                return View(NewView);
            }
            return View();
        }
    }
}
