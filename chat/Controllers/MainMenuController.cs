using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chat.Controllers
{
    [Authorize]
    public class MainMenuController : Controller
    {
        public IActionResult Hello()
        {
            return View();
        }
    }
}
