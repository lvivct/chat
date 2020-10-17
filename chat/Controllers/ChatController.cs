using System.Linq;
using System.Security.Claims;
using chat.Models;
using chat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace chat.Controllers
{
    [Authorize]
    //[Route("[Controller]")]
    public class ChatController : Controller
    {
        private readonly AppDbContext AppDb;
        public ChatController(AppDbContext appDb)
        {
            AppDb = appDb;
        }

        //[Route("[Action]")]
        public IActionResult AllChats()
        {
            var CurrentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var AllUsers = AppDb.Users
                        .Include(x => x.AppUsersChats)
                        .ThenInclude(x => x.Chat);

            var CurrentUser = AllUsers.ToList().Find(e => e.Id == CurrentUserId);
            var Chats = CurrentUser.AppUsersChats.ToList();
            return View(Chats);
        }


        //[HttpGet("[Action]")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //[HttpPost("[Action]")]
        [HttpPost]
        public IActionResult Create(Chat model)
        {
            if (ModelState.IsValid)
            {
                Chat newchat = new Chat
                {
                    ChatName = model.ChatName,
                };
                AppDb.ChatsDatabase.Add(newchat);
                AppDb.SaveChanges();

                AppUserChat newAppUserChat = new AppUserChat
                {
                    ChatId = newchat.ChatId,
                    UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value
            };
                AppDb.ChatsUsersDatabase.Add(newAppUserChat);
                AppDb.SaveChanges();
                return RedirectToAction("AllChats", "Chat");
            }
            return View();
        }

        //[HttpGet("{chatId}")]
        [HttpGet]
        public IActionResult Open(string chatId)
        {
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

        //[HttpGet("[Action]/{chatId}")]
        [HttpGet]
        public IActionResult AddMemder(string chatId)
        {
            AddUserViewModel NewView = new AddUserViewModel
            {
                ChatId = chatId
            };
            return View(NewView);
        }

        //[HttpPost("[Action]/{chatId}")]
        [HttpPost]
        public IActionResult AddMemder(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = AppDb.Users
                    .Where(b => b.UserName == model.UserName)
                    .FirstOrDefault();

                short exeption = 0;
                if (User == null)
                    exeption = 1;
                else
                {
                    AppUserChat newAppUserChat = new AppUserChat
                    {
                        ChatId = model.ChatId,
                        UserId = User.Id
                    };
                    foreach (var x in AppDb.ChatsUsersDatabase.ToList())
                        if (x.ChatId == newAppUserChat.ChatId && x.UserId == newAppUserChat.UserId)
                        {
                            exeption = 2;
                        }
                    if (exeption == 0)
                    {
                        AppDb.ChatsUsersDatabase.Add(newAppUserChat);
                        AppDb.SaveChanges();
                        return RedirectToAction("AllChats", "Chat");
                    }
                }
                if (exeption == 1)
                    ModelState.AddModelError(string.Empty, "This User doesn't exist");
                else
                    ModelState.AddModelError(string.Empty, "This User already added");
            }
            AddUserViewModel NewView = new AddUserViewModel
            {
                ChatId = model.ChatId
            };
            return View(NewView);
        }
    }
}