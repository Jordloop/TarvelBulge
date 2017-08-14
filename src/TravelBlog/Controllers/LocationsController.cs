using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelBlog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TravelBlog.Controllers
{
    public class LocationsController : Controller
    {
        private readonly TravelBlogContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public LocationsController(UserManager<ApplicationUser> userManager, TravelBlogContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Locations.ToList());
        }

        //Details
        public IActionResult Details(int Id)
        {
            var thisLocation = _db.Locations.Include(locations => locations.Experiences).FirstOrDefault(locations => locations.LocationId == Id);

            //ICollection<Experience> thisLocationExperiences = thisLocation.Experiences;
            return View(thisLocation);
        }

        //Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Location location)
        {
            _db.Locations.Add(location);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Edit
        public IActionResult Edit(int id)
        {
            var thisLocation = _db.Locations.FirstOrDefault(locations => locations.LocationId == id);
            return View(thisLocation);
        }

        [HttpPost]
        public IActionResult Edit(Location location)
        {
            _db.Entry(location).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Delete
        public IActionResult Delete(int id)
        {
            var thisLocation = _db.Locations.FirstOrDefault(items => items.LocationId == id);
            return View(thisLocation);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisLocation = _db.Locations.FirstOrDefault(items => items.LocationId == id);
            _db.Locations.Remove(thisLocation);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
