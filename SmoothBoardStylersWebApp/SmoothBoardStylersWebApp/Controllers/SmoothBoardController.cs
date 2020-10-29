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
        private readonly SmoothBoardContext _db;

        public SmoothBoardController(SmoothBoardContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var smoothBoards = _db.SmoothBoards.ToList();
            return View(smoothBoards);
        }
    }
}
