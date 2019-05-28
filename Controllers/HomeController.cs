using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ActivityCenter.Models;

namespace ActivityCenter.Controllers
{
    public class HomeController : Controller
    {
        private DojoContext context;
        
        public HomeController(DojoContext da)
        {
            context = da;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var dbuser = context.Users.FirstOrDefault(u => u.Email == user.Email);
                if(context.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");    
                    return View("Index");                
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);                
                context.Add(user);
                context.SaveChanges();
                HttpContext.Session.SetInt32("UserId", user.UserId);
                // 
                
                return RedirectToAction("Dashboard","Activity");
            }
            return View("Index");
        }
        [HttpPost("login")]
        public IActionResult Login(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                var dbuser = context.Users.FirstOrDefault(u => u.Email == user.LogEmail);
                if(dbuser == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password!");    
                    return View("Index");                
                }
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(user, dbuser.Password, user.LogPassword);
                if(result == 0)
                {
                    ModelState.AddModelError("Password", "Invalid Email/Password!");
                    return View("Index");
                }
                HttpContext.Session.SetInt32("UserId", dbuser.UserId);

                return RedirectToAction("Dashboard","Activity");
            }
            return View("Index");
        }
        
    }
}
