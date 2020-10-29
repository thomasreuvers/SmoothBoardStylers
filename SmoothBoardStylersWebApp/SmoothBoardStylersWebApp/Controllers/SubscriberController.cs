using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SmoothBoardStylersWebApp.Controllers
{
    public class SubscriberController : Controller
    {

        [HttpPost]
        public async Task<IActionResult> AddSubscriber()
        {

            return RedirectToAction("Index", "Home");
        }
    }
}
