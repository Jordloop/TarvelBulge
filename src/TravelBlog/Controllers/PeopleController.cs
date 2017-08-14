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
    public class PeopleController : Controller
    {
        private readonly TravelBlogContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public PeopleController(UserManager<ApplicationUser> userManager, TravelBlogContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.People.Include(people => people.Experience).ToList());
        }
        public IActionResult Details(int id)
        {
            var thisPeople = _db.People.FirstOrDefault(people => people.PeopleId == id);
            return View(thisPeople);
        }
        //Create
        public IActionResult Create()
        {
            ViewBag.ExperienceId = new SelectList(_db.Experiences, "ExperienceId", "Story");
            return View();
        }
        [HttpPost]
        public IActionResult Create(People people)
        {
            _db.People.Add(people);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Edit
        public IActionResult Edit(int id)
        {
            var thisPeople = _db.People.FirstOrDefault(people => people.PeopleId == id);
            ViewBag.ExperienceId = new SelectList(_db.Experiences, "ExperienceId", "Story");
            return View(thisPeople);
        }
        [HttpPost]
        public IActionResult Edit(People people)
        {
            _db.Entry(people).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Delete
        public ActionResult Delete(int id)
        {
            var thisPeople = _db.People.FirstOrDefault(people => people.PeopleId == id);
            return View(thisPeople);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisPeople = _db.People.FirstOrDefault(people => people.PeopleId == id);
            _db.People.Remove(thisPeople);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
