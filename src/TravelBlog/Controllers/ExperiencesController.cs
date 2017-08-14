using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelBlog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace TravelBlog.Controllers
{
    public class ExperiencesController : Controller
    {

        private readonly TravelBlogContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExperiencesController(UserManager<ApplicationUser> userManager, TravelBlogContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Experiences.Include(experiences => experiences.Location).ToList());
        }
        //Details
        public IActionResult Details(int id)
        {
            var thisExperience = _db.Experiences.Include(experiences => experiences.People).FirstOrDefault(experiences => experiences.ExperienceId == id);
            return View(thisExperience);
        }
        //Create 
        public IActionResult Create()
        {
            ViewBag.LocationId = new SelectList(_db.Locations, "LocationId", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Experience item)
        {
            _db.Experiences.Add(item);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Edit
        public IActionResult Edit(int id)
        {
            var thisExperience = _db.Experiences.FirstOrDefault(experiences => experiences.ExperienceId == id);
            ViewBag.LocationId = new SelectList(_db.Locations, "LocationId", "Name");
            return View(thisExperience);
        }
        [HttpPost]
        public IActionResult Edit(Experience item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Delete
        public ActionResult Delete(int id)
        {
            var thisExperience = _db.Experiences.FirstOrDefault(experiences => experiences.ExperienceId == id);
            return View(thisExperience);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisExperience = _db.Experiences.FirstOrDefault(experiences => experiences.ExperienceId == id);
            _db.Experiences.Remove(thisExperience);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
