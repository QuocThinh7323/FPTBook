using FPTBook.Models;
using FPTBookDemo.Data;
using FPTBookDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace FPTBookDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var applicationDbContext = _context.Book.Include(b => b.Author).Include(b => b.Category).Include(b => b.PublishingCompany);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Search(string searchTerm)
        {
            // Tìm kiếm sách trong cơ sở dữ liệu dựa trên searchTerm
            var searchResults = await _context.Book
                .Where(book => book.Name.Contains(searchTerm) || book.Description.Contains(searchTerm))
                .ToListAsync();

            // Trả về kết quả tìm kiếm
            return View(searchResults);
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Help()
        {
            return View();
        }

        [HttpGet("{id}")]
        public IActionResult Detail(int id)
        {
            Book book = _context.Book.Include(b => b.Category).FirstOrDefault(b => b.Id == id);
            return View(book);
        }

        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public async Task<IActionResult> Shop()
        {
            var applicationDbContext = _context.Book.Include(b => b.Author).Include(b => b.Category).Include(b => b.PublishingCompany);
            return View(await applicationDbContext.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}