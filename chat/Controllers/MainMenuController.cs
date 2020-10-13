using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
