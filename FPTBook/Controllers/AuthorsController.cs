using Microsoft.AspNetCore.Mvc;

namespace FPTBook.Controllers
{
    public class AuthorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
