using Microsoft.AspNetCore.Mvc;

namespace FPTBook.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
