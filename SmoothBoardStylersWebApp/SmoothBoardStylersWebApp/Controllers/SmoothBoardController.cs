using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmoothBoardStylersWebApp.ExtensionMethods;
using SmoothBoardStylersWebApp.Models;

namespace SmoothBoardStylersWebApp.Controllers
{
    public class SmoothBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
