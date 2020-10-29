using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmoothBoardStylersWebApp.ExtensionMethods;
using SmoothBoardStylersWebApp.Models;
using SmoothBoardStylersWebApp.Services;

namespace SmoothBoardStylersWebApp.Controllers
{
    public class SubscriberController : Controller
    {
        private readonly MailService _mailService;
        private readonly SmoothBoardContext _db;

        public SubscriberController(MailService mailService, SmoothBoardContext db)
        {
            _mailService = mailService;
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> AddSubscriber(SubscriberViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Home");


            // Subscriber already in database redirect to index
            if (_db.Subscribers.Any(x => x.EmailAddress == model.EmailAddress))
            {
                RedirectToAction("Index", "Home");
            }

            // Add new subscriber to database
            var key = "".CreateKey(8);
            var subscriber = new SubscriberModel
            {
                EmailAddress = model.EmailAddress,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Sex = model.Sex,
                SubscriberKey = key
            };

            await _db.Subscribers.AddAsync(subscriber);
            await _db.SaveChangesAsync();

            subscriber = _db.Subscribers.FirstOrDefault(x => x.SubscriberKey == key);

            // Is the subscriber in the database?
            if (subscriber != null)
            {
                _mailService.SendMailAsync(subscriber.EmailAddress, "Inschrijven nieuwsbrief", $"Beste {subscriber.Firstname}, <br>Bedankt voor het inschrijve voor de nieuwsbrief!<br>Je kunt je gemakkelijk uitschrijven doormiddel van deze link: https://localhost:44361/subscriber/removesubscriber/?id={subscriber.SubscriberId}");

                TempData.Add("addSubscriber", true);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> RemoveSubscriber(int? id)
        {
            if (!id.HasValue) return RedirectToAction("Index", "Home");

            // Does the user with that id exist in the DB?
            var subscriber = _db.Subscribers.FirstOrDefault(x => x.SubscriberId == id.Value);
            if (subscriber == null) return RedirectToAction("Index", "Home");

            _db.Remove(subscriber);
            await _db.SaveChangesAsync();

            _mailService.SendMailAsync(subscriber.EmailAddress, "Uitschrijven nieuwsbrief", $"Jammer dat u weg gaat {subscriber.Firstname}!");

            return RedirectToAction("Index", "Home");
        }
    }
}
