using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SmoothBoardStylersWebApp.ExtensionMethods;
using SmoothBoardStylersWebApp.Models;

namespace SmoothBoardStylersWebApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly SmoothBoardContext _db;

        public AdminController(IWebHostEnvironment hostEnvironment, SmoothBoardContext db)
        {
            _hostEnvironment = hostEnvironment;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSmoothBoard(SmoothBoardViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData.Add("AddedSmoothBoard", false);
                return RedirectToAction("Index", "Admin");
            }

            if (!Directory.Exists(Path.Combine(_hostEnvironment.WebRootPath, "uploads")))
            {
                Directory.CreateDirectory(Path.Combine(_hostEnvironment.WebRootPath, "uploads"));
            }

            var fileName = GetUniqueFileName(viewModel.SmoothBoardImage.FileName) ;
            var filePath = Path.Combine(Path.Combine(_hostEnvironment.WebRootPath, "uploads"), fileName);
            await viewModel.SmoothBoardImage.CopyToAsync(new FileStream(filePath, FileMode.Create));

            await _db.SmoothBoards.AddAsync(new SmoothBoardModel
            {
                Description = viewModel.Description,
                ImageUrl = fileName,
                Price = viewModel.Price,
                Model = viewModel.Model
            });

            await _db.SaveChangesAsync();

            TempData.Add("AddedSmoothBoard", true);

            return RedirectToAction("Index", "Admin");
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_"
                   + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }
    }
}
