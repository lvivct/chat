using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using chat.Models;
using chat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace chat.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext AppDb;

        public UserController(AppDbContext appDb, IWebHostEnvironment webHostEnvironment,
            SignInManager<AppUser> signInManager, UserManager<AppUser> userManager )
        {
            AppDb = appDb;
            _webHostEnvironment = webHostEnvironment;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var CurrentUser = await _userManager.GetUserAsync(HttpContext.User);

            EditUserViewModel model = new EditUserViewModel
            {
                PhotoPath = CurrentUser.Photopath,
                UserId = CurrentUser.Id,
                UserName = CurrentUser.UserName,
                Email = CurrentUser.Email
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            var CurrentUser = AppDb.Find<AppUser>(model.UserId);
            if (CurrentUser.Email != model.Email || CurrentUser.UserName != model.UserName || model.Photo != null)
            {
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    CurrentUser.Photopath = uniqueFileName;
                }
                if (CurrentUser.Email != model.Email)
                    CurrentUser.Email = model.Email;
                if (CurrentUser.UserName != model.UserName)
                {
                    CurrentUser.UserName = model.UserName;

                    var messages = AppDb.MessagesDatabase;
                    foreach (var message in messages)
                        if (message.SenderId == model.UserId)
                            message.SenderName = model.UserName;
                }
                await _userManager.UpdateAsync(CurrentUser);
                await AppDb.SaveChangesAsync();
            }
            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var result = await _userManager.ChangePasswordAsync(user,
                    model.CurrentPassword, model.NewPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToAction("Edit");
            }
            return View(model);
        }

        [HttpPost]        
        public async Task<IActionResult> DeleteUser()
        {
            var CurrentUser = await _userManager.GetUserAsync(HttpContext.User);

            var _currentUser = AppDb.Users.Include(e => e.AppUsersChats).
                ThenInclude(e => e.Chat)
                .ThenInclude(e => e.Messages)
                .ToList().Find(e => e.Id == CurrentUser.Id);

            var CurrentUserChats = _currentUser.AppUsersChats;
            foreach (var userchat in CurrentUserChats.ToList())
            {
                if (1 == userchat.Chat.AppUsersChats.Count())
                    AppDb.ChatsDatabase.Remove(userchat.Chat);
                else
                {
                    foreach (var Message in userchat.Chat.Messages)
                        if (CurrentUser.Id == Message.SenderId)
                        {
                            Message.SenderId = "0";
                            Message.SenderName = "deleted account";
                        }
                }
            }
            AppDb.Users.Remove(CurrentUser);
            await AppDb.SaveChangesAsync();
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
