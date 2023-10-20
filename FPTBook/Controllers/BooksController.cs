using Microsoft.AspNetCore.Mvc;

namespace FPTBook.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
