using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmoothBoardStylersWebApp.Models;

namespace SmoothBoardStylersWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SmoothBoardContext _db;

        public AccountController(SmoothBoardContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
