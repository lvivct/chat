﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using chat.Models;
using chat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;

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

                AppUserChat newAppUserChat = new AppUserChat("admin")
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
            var CurrentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            ChatViewModel NewView = new ChatViewModel
            {
                ChatId = CurrentChat.ChatId,
                SenderId = CurrentUserId,
                ChatName = CurrentChat.ChatName,
                Messages = CurrentChat.Messages
            };
            return View(NewView);
        }

        //[HttpGet("[Action]/{chatId}")]
        [HttpGet]
        public IActionResult AddMemder(string chatId)
        {
            var ChatsUser = AppDb.ChatsUsersDatabase.Find
                (chatId, User.FindFirst(ClaimTypes.NameIdentifier).Value);
            AddUserViewModel ViewModel = new AddUserViewModel
            {
                ChatId = chatId,
                AddUsers = ChatsUser.AddUsers
            };
            return View(ViewModel);
        }

        //[HttpPost("[Action]/{chatId}")]
        [HttpPost]
        public IActionResult AddMemder(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = AppDb.Users
                    .Where(b => b.UserName == model.UserName)
                    .First();

                if (User == null)
                    ModelState.AddModelError(string.Empty, "This User doesn't exist");
                else
                {
                    AppUserChat newAppUserChat = new AppUserChat
                    {
                        ChatId = model.ChatId,
                        UserId = User.Id
                    };
                    if (AppDb.ChatsUsersDatabase.Find(model.ChatId, User.Id) == null)
                    {
                        AppDb.ChatsUsersDatabase.Add(newAppUserChat);
                        AppDb.SaveChanges();
                        return RedirectToAction("AllChats", "Chat");
                    }
                }
                ModelState.AddModelError(string.Empty, "This User already added");
            }
            return View(model);
        }

        [HttpPost]  
        public IActionResult Leave(string chatId)
        {
            var CurrentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var _currentUser = AppDb.Users.Include(e => e.AppUsersChats).
                ThenInclude(e => e.Chat)
                .ThenInclude(e => e.Messages)
                .ToList().Find(e => e.Id == CurrentUserId);

            var CurrentUserChats = _currentUser.AppUsersChats;
            foreach (var userchat in CurrentUserChats.ToList())
                if (userchat.ChatId == chatId && userchat.UserId == CurrentUserId)
                {
                    if (1 == userchat.Chat.AppUsersChats.Count())
                        AppDb.ChatsDatabase.Remove(userchat.Chat);
                    else
                        AppDb.ChatsUsersDatabase.Remove(userchat);
                    AppDb.SaveChanges();
                    break;
                }
            return RedirectToAction("AllChats");
        }

        [HttpGet]
        public IActionResult EditChat(string chatId)
        {
            var thisChat = AppDb.ChatsDatabase.Include(e => e.AppUsersChats)
                                         .ThenInclude(e => e.User)
                                         .ToList().Find(e => e.ChatId == chatId);

            var allUsersChats = thisChat.AppUsersChats.ToList();

            var CurrentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var CurrentUserChat = allUsersChats.Find(e => e.UserId == CurrentUserId);


            var allUsersNames = new List<string>();
            var allUsersIds = new List<string>();
            for (int i = 0; i < allUsersChats.Count; ++i)
            {
                allUsersNames.Add(allUsersChats[i].User.UserName);
                allUsersIds.Add(allUsersChats[i].UserId);
            }

            var viewmodel = new ChatEditViewModel
            {
                ChatId = chatId,
                ChatName = thisChat.ChatName,
                UserNameList = allUsersNames,
                UserIdList = allUsersIds,
                EditChat = CurrentUserChat.EditChat,
                KickUsers = CurrentUserChat.KickUsers,
                GiveRoles = CurrentUserChat.GiveRoles
            };
            return View(viewmodel);
        }
        
        [HttpPost]
        public async Task<IActionResult> EditChat(string chatId, string chatName)
        {
            var thisChat = AppDb.ChatsDatabase.ToList().Find(e => e.ChatId == chatId);
            thisChat.ChatName = chatName;
            await AppDb.SaveChangesAsync();

            return RedirectToAction("Open", new { chatId });
        }

        [HttpPost]
        public async Task<IActionResult> KickUser(string chatId, string userId)
        {
            var thisChat = AppDb.ChatsDatabase.Include(e => e.AppUsersChats);
            var UserChat = new AppUserChat
            {
                ChatId = chatId,
                UserId = userId
            };
            AppDb.ChatsUsersDatabase.Remove(UserChat);
            await AppDb.SaveChangesAsync();

            return RedirectToAction("EditChat", new { chatId });
        }

        [HttpGet]
        public IActionResult ChangeRole(string chatId, string userId)
        {
            var model = AppDb.ChatsUsersDatabase.Find(chatId, userId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(AppUserChat model)
        {
            var userRole = AppDb.ChatsUsersDatabase.Find(model.ChatId, model.UserId);
            userRole.RoleName = model.RoleName;
            userRole.GiveRoles = model.GiveRoles;
            userRole.KickUsers = model.KickUsers;
            userRole.EditChat = model.EditChat;
            userRole.AddUsers = model.AddUsers;
            await AppDb.SaveChangesAsync();
            return RedirectToAction("EditChat", new { model.ChatId });
        }

        
        [HttpPost]
        public async Task<IActionResult> DeleteChat(string chatId)
        {
            var thisChat = AppDb.ChatsDatabase.Include(e => e.AppUsersChats)
                                              .ToList().Find(e => e.ChatId == chatId);
            foreach(var AppUsersChat in thisChat.AppUsersChats)
                AppDb.ChatsUsersDatabase.Remove(AppUsersChat);
            await AppDb.SaveChangesAsync();
            return RedirectToAction("AllChats");
        }
    }
}