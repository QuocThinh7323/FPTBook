using Microsoft.AspNetCore.Mvc;

namespace FPTBook.Controllers
{
    public class PublishingCompaniesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
