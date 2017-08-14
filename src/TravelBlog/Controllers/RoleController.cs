using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using TravelBlog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelBlog.Controllers
{
 
        public class RolesController : Controller
        {
            private readonly TravelBlogContext _db;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly SignInManager<ApplicationUser> _signInManager;
            public RolesController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, TravelBlogContext db)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _db = db;
            }
            public IActionResult Index()
            {
                var roles = _db.Roles.ToList();
                return View(roles);
            }
            public ActionResult Create()
            {
                return View();
            }

            //
            // POST: /Roles/Create
            [HttpPost]
            public ActionResult Create(string RoleName)
            {
                try
                {
                    _db.Roles.Add(new IdentityRole()
                    {
                        Name = RoleName,
                        NormalizedName = RoleName.ToUpper()
                    });
                    _db.SaveChanges();
                    ViewBag.ResultMessage = "Role created successfully !";
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            public ActionResult Edit(string RoleName)
            {
                var thisRole = _db.Roles.FirstOrDefault(r => r.Name == RoleName);

                return View(thisRole);
            }

            //
            // POST: /Roles/Edit/5

            public ActionResult Delete(string RoleName)
            {
                var thisRole = _db.Roles.FirstOrDefault(r => r.Name == RoleName);
                _db.Roles.Remove(thisRole);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            //
            // POST: /Roles/Edit/5
            [HttpPost, ActionName("Edit")]
            [ValidateAntiForgeryToken]
            public ActionResult EditRole(IdentityRole role)
            {
                try
                {
                    var thisRole = _db.Roles.FirstOrDefault(r => r.Id == role.Id);
                    thisRole.Name = role.Name;
                    thisRole.NormalizedName = role.Name.ToUpper();
                    _db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return View();
                }
            }
            public IActionResult ManageUserRoles()
            {
                // prepopulat roles for the view dropdown
                var list = _db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> RoleAddToUser(string UserName, string Roles)
            {
                ApplicationUser user = _db.Users.FirstOrDefault(u => u.UserName == UserName);
                await _userManager.AddToRoleAsync(user, Roles);
                ViewBag.ResultMessage = "Role created successfully !";
                var list = _db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;

                return View("ManageUserRoles");
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> GetRoles(string UserName)
            {
                if (!string.IsNullOrWhiteSpace(UserName))
                {
                    ApplicationUser user = await _userManager.FindByNameAsync(UserName);
                    ViewBag.RolesForThisUser = await _userManager.GetRolesAsync(user);
                    var list = _db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                    ViewBag.Roles = list;
                    ViewBag.UserName = new SelectList(_db.Users, "UserName", "UserName");
                }
                return View("ManageUserRoles");
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteRoleForUser(string UserName, string Roles)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(UserName);

                if (await _userManager.IsInRoleAsync(user, Roles))
                {
                    await _userManager.RemoveFromRoleAsync(user, Roles);
                }
                else
                {
                    ViewBag.ResultMessage = "This user doesn't have that role.";
                }
                var list = _db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
                ViewBag.UserName = new SelectList(_db.Users, "UserName", "UserName");

                return View("ManageUserRoles");
            }
        }
    }