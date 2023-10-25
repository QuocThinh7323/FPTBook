using FPTBook.Models;
using FPTBookDemo.Data;
using FPTBookDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FPTBook.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Owner")]
    [Area("Admin")]
    public class UsersListController : Controller
    {        
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UsersListController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        //GET: UsersList/searchstr
        // GET: UsersList
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }
        public async Task<IActionResult> Search(string searchstr)
        {
            var search = searchstr.Trim();
            var users = await _context.Users.Where(u => u.UserName.Contains(search)).ToListAsync();
            return View(users);
        }

        //GET: SetPassword
        public IActionResult SetPassword(string id) 
        {
            ViewBag.UID = id;
            return View();
        }

        //POST: UsersController/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword([Bind("UID, NewPassword, ConfirmPassword")] SetPasswordViewModel pass)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = await _userManager.FindByIdAsync(pass.UID);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID'{pass.UID}'.");
            }
            await _userManager.RemovePasswordAsync(user);
            var addPasswordResult = await _userManager.AddPasswordAsync(user, pass.NewPassword);
            if (addPasswordResult.Succeeded)
            {
                foreach (var error in addPasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}
