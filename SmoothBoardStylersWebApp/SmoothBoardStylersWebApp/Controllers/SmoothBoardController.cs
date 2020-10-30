using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmoothBoardStylersWebApp.ExtensionMethods;
using SmoothBoardStylersWebApp.Models;
using SmoothBoardStylersWebApp.Services;

namespace SmoothBoardStylersWebApp.Controllers
{
    public class SmoothBoardController : Controller
    {
        private readonly SmoothBoardContext _db;
        private readonly MailService _mailService;

        public SmoothBoardController(SmoothBoardContext db, MailService mailService)
        {
            _db = db;
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            var smoothBoards = _db.SmoothBoards.ToList();
            return View(smoothBoards);
        }

        [HttpPost]
        public async Task<IActionResult> MoreInformation(string ModelName, string Name, string PhoneNumber)
        {
            if (!string.IsNullOrEmpty(ModelName) && !string.IsNullOrEmpty(PhoneNumber) && !string.IsNullOrEmpty(Name))
            {
                _mailService.SendSelfMailAsync("Nieuwe geinteresseerde klant!",
                    $"Er is een nieuwe geinteresseerde klant!<br/> <b>Naam:</b> {Name}<br/><b>Telefoonnummer:</b> {PhoneNumber}<br/><b>Model:</b> {ModelName}");
            }


            return RedirectToAction("Index");
        }
    }
}
