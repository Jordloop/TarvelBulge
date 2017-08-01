using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelBlog.Models;

namespace TravelBlog.Controllers
{
    public class LocationsController : Controller
    {
        private TravelBlogContext db = new TravelBlogContext();
        public IActionResult Index()
        {
            return View(db.Locations.ToList());
        }

        //CREATE
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
    }
}
