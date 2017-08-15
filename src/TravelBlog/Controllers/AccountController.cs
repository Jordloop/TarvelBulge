using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TravelBlog.Models;
using System.Threading.Tasks;
using TravelBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicAuthentication.Controllers
{
    public class AccountController : Controller
    {
        private TravelBlogContext db = new TravelBlogContext();
        private readonly TravelBlogContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, TravelBlogContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [Route("/Account/Register")]
        [HttpPost]
        public async Task<IActionResult> Register(string Email, string Password)
        {
            ApplicationUser user = new ApplicationUser { Email = Email, UserName = Email };
            IdentityResult result = await _userManager.CreateAsync(user, Password);
            if (result.Succeeded)
            {
                return Json(user);
            }
            else
            {
                return View();
            }
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public IActionResult NewUser(RegisterViewModel model)
        //{
        //    var user = new ApplicationUser(email, password, confirmPassword);
        //    _db.user.Add(user);
        //    _db.SaveChanges();
        //    return Json(user);
        //}
    }
}