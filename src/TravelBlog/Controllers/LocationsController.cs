using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace TravelBlog.Controllers
{
    public class LocationsController : Controller
    {
        //Instantiate database object

        private TravelBlogContext db = new TravelBlogContext();
        public IActionResult Index()
        {
            return View(db.Locations.ToList());
        }

        //Details
        public IActionResult Details(int Id)
        {
            var thisLocation = db.Locations.Include(locations => locations.Experiences).FirstOrDefault(locations => locations.LocationId == Id);

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
            db.Locations.Add(location);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Edit
        public IActionResult Edit(int id)
        {
            var thisLocation = db.Locations.FirstOrDefault(locations => locations.LocationId == id);
            return View(thisLocation);
        }

        [HttpPost]
        public IActionResult Edit(Location location)
        {
            db.Entry(location).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Delete
        public IActionResult Delete(int id)
        {
            var thisLocation = db.Locations.FirstOrDefault(items => items.LocationId == id);
            return View(thisLocation);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisLocation = db.Locations.FirstOrDefault(items => items.LocationId == id);
            db.Locations.Remove(thisLocation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
