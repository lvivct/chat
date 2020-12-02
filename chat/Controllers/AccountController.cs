using chat.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace chat.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager; 
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // [HttpPost("[Action]")] temp
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Photopath = "~/images/no_avatar.png"
                };
                var result = await userManager.CreateAsync(user, model.Password);       
                if (result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, token = token }, Request.Scheme);



                    MimeMessage message = new MimeMessage();

                    MailboxAddress from = new MailboxAddress("Cchat", "admin@example.com");
                    message.From.Add(from);
                    MailboxAddress to = new MailboxAddress(model.UserName, model.Email);
                    message.To.Add(to);
                    message.Subject = "Confirm your email";

                    BodyBuilder bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = "Welcome to the Cchat Thanks for joining!\n" + 
                        "Please verify your email address to get access to Cchat " +  confirmationLink;

                    message.Body = bodyBuilder.ToMessageBody();

                    /*SmtpClient smtp = new SmtpClient();
                    smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    smtp.Authenticate("viktor01444", "viktorxx2001");
                    usmtp.Send(message);
                    smtp.Disconnect(true);*/

                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Hello", "MainMenu");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.UserName, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Hello", "MainMenu");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
                return View("NotFound");
            }

            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View();
            }

            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return View("Error");
        }
    }
}
