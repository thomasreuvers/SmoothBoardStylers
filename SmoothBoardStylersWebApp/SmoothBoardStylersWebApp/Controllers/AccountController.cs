using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View();

            var user = _db.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            if (user == null)
            {
                ModelState.AddModelError("InvalidPasswordOrEmail", "Password or Email is invalid!");
                return View();
            }

            /* Create the identity of the user
             * 
             */
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
            identity.AddClaim(new Claim(user.Role, user.Role));

            // Sign user in
            var principle = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);

            // Redirect to Admin panel
            return RedirectToAction("Index", "Admin");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
