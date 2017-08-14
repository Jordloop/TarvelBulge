using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelBlog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace TravelBlog.Controllers
{
    public class SuggestionsController : Controller
    {

        private readonly TravelBlogContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public SuggestionsController(UserManager<ApplicationUser> userManager, TravelBlogContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            return View(_db.Suggestions.Where(x => x.User.Id == currentUser.Id));
        }
        public IActionResult Create()
        {
            ViewBag.LocationId = new SelectList(_db.Locations, "LocationId", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Suggestion suggestion, int locationId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            suggestion.User = currentUser;
            var thisLocation = _db.Locations.FirstOrDefault(locations => locations.LocationId == locationId);
            suggestion.Location = thisLocation;
            suggestion.Visted = false;
            suggestion.UpVote = 0;
            _db.Suggestions.Add(suggestion);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var thisSuggestion = _db.Suggestions.Include(suggestions => suggestions.Location).FirstOrDefault(suggestions => suggestions.SuggestionId == id);
            return View(thisSuggestion);
        }
        //Create 

        public IActionResult Edit(int id)
        {
            var thisSuggestion = _db.Suggestions.FirstOrDefault(suggestions => suggestions.SuggestionId == id);
            ViewBag.LocationId = new SelectList(_db.Locations, "LocationId", "Name");
            return View(thisSuggestion);
        }
        [HttpPost]
        public IActionResult Edit(Suggestion suggestion)
        {
            _db.Entry(suggestion).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Delete
        public ActionResult Delete(int id)
        {
            var thisSuggestion = _db.Suggestions.FirstOrDefault(suggestions => suggestions.SuggestionId == id);
            return View(thisSuggestion);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisSuggestion = _db.Suggestions.FirstOrDefault(suggestions => suggestions.SuggestionId == id);
            _db.Suggestions.Remove(thisSuggestion);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
