using System.Threading.Tasks;
using chat.Models;
using chat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace chat.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext AppDb;

        public UserController(AppDbContext appDb,
            SignInManager<AppUser> signInManager, UserManager<AppUser> userManager )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            AppDb = appDb;
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var CurrentUser = await _userManager.GetUserAsync(HttpContext.User);
            EditUserViewModel model = new EditUserViewModel
            {
                UserName = CurrentUser.UserName,
                Email = CurrentUser.Email
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var CurrentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (CurrentUser.Email != model.Email || CurrentUser.UserName != model.UserName)
                { 
                    CurrentUser.Email = model.Email;
                    CurrentUser.UserName = model.UserName;
                    await _userManager.UpdateAsync(CurrentUser);
                }
                await AppDb.SaveChangesAsync();
            }
            return RedirectToAction("Hello","MainMenu");
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
    }
}
