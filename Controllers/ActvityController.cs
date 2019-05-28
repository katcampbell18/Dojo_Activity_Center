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
    public class ActivityController : Controller
    {
        private DojoContext context;
        
        public ActivityController(DojoContext da)
        {
            context = da;
        }
        [HttpGet("new")]
        public IActionResult New()
        {
            return View();
        }
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }
        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            string[] keys = HttpContext.Session.Keys.ToArray();
            if(keys.Contains("UserId"))
            {
                User User = context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
                List<DActivity> Activities = context.Activities.Include(a => a.Planner).Include(p => p.Participants).ThenInclude(u => u.User).ToList();
                ViewBag.Activities = Activities;
                ViewBag.User = User;
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost("create")]
        public IActionResult Create(DActivity active)
        {
            if(ModelState.IsValid)
            {
                DateTime ActivityDate = DateTime.Parse(active.Date);
                DateTime CurrentDate = DateTime.Today;
                if(ActivityDate < CurrentDate){
                    User User1 = context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
                    ViewBag.User = User1;
                    ModelState.AddModelError("Date", "Date cannot be in the past");
                    return View("New");
                }
                active.UserId = (int)HttpContext.Session.GetInt32("UserId");
                Join newJoin = new Join();
                context.Activities.Add(active);
                context.SaveChanges();
                newJoin.ActivityId = active.ActivityId;
                newJoin.UserId = active.UserId;
                context.Joins.Add(newJoin);
                context.SaveChanges();
                // return Redirect("Dashboard");
                return Redirect($"activity/{active.ActivityId}");
            }
            User User = context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.User = User;
            return View("New");
        }
        [HttpGet("activity/{id}")]
        public IActionResult Show(int id)
        {
            string[] keys = HttpContext.Session.Keys.ToArray();
            if(keys.Contains("UserId"))
            {
                User User = context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
                DActivity Activity = context.Activities.Include(x => x.Planner).Include(y => y.Participants).ThenInclude(z => z.User).FirstOrDefault(act => act.ActivityId == id);
                ViewBag.Activity = Activity;
                ViewBag.User = User;
                return View("Display");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    [HttpGet("delete/{id}")]

        public IActionResult Delete(int id)
        {
            DActivity Activity = context.Activities.FirstOrDefault(a => a.ActivityId == id);
            context.Activities.Remove(Activity);
            context.SaveChanges();
            return RedirectToAction("Dashboard","Activity");
        }

        [HttpGet("join/{activity_id}/{user_id}")]

        public IActionResult Join(int activity_id, int user_id)
        {
            Join Join = new Join();
            Join.UserId = user_id;
            Join.ActivityId = activity_id;
            context.Joins.Add(Join);
            context.SaveChanges();
            return RedirectToAction("Dashboard","Activity");
        }

        [HttpGet("leave/{activity_id}/{user_id}")]

        public IActionResult Leave(int activity_id, int user_id)
        {
            Join Join = context.Joins.FirstOrDefault(a => a.ActivityId == activity_id && a.UserId == user_id);
            context.Joins.Remove(Join);
            context.SaveChanges();
            return RedirectToAction("Dashboard","Activity");
        }

    }
}