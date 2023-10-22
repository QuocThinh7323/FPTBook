using FPTBook.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FPTBook.Controllers
{
    public class PublishingCompaniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PublishingCompaniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PublishingCompanies
        public async Task<IActionResult> Index()
        {
            return _context.PublishingCompanies != null ?
                        View(await _context.PublishingCompanies.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.PublishingCompanies'  is null.");
        }

        // GET: PublishingCompanies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PublishingCompanies == null)
            {
                return NotFound();
            }

            var PublishingCompanies = await _context.PublishingCompanies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (PublishingCompanies == null)
            {
                return NotFound();
            }

            return View(PublishingCompanies);
        }

        // GET: PublishingCompanies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PublishingCompanies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PublishingCompanies publishingCompanies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(PublishingCompanies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(PublishingCompanies);
        }

        // GET: PublishingCompanies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PublishingCompanies == null)
            {
                return NotFound();
            }

            var PublishingCompanies = await _context.PublishingCompanies.FindAsync(id);
            if (PublishingCompanies == null)
            {
                return NotFound();
            }
            return View(PublishingCompanies);
        }

        // POST: PublishingCompanies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PublishingCompanies publishingCompanies)
        {
            if (id != PublishingCompanies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(PublishingCompanies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublishingCompanyExists(PublishingCompanies.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(PublishingCompanies);
        }

        // GET: PublishingCompanies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PublishingCompanies == null)
            {
                return NotFound();
            }

            var PublishingCompanies = await _context.PublishingCompanies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (PublishingCompanies == null)
            {
                return NotFound();
            }

            return View(PublishingCompanies);
        }

        // POST: PublishingCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PublishingCompanies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PublishingCompany'  is null.");
            }
            var PublishingCompanies = await _context.PublishingCompanies.FindAsync(id);
            if (PublishingCompanies != null)
            {
                _context.PublishingCompanies.Remove(PublishingCompanies);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublishingCompanyExists(int id)
        {
            return (_context.PublishingCompanies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
