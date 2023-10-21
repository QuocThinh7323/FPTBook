using FPTBook.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FPTBook.Controllers
{
    public class UsersList
    {
        [Authorize(Roles = "Admin")]
        public class UsersListController : Controller
        {
            private readonly ApplicationDbContext _context;
            private readonly UserManager<ApplicationUser> _userManager;
            public UsersListController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
            {
                _context = context;
                _userManager = userManager;
            }

            //GET: UsersController
            public async Task<IActionResult> Index()
            {
                return View(await _context.Users.ToListAsync());
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
}
