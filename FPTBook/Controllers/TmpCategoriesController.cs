using FPTBook.Data;
using FPTBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FPTBook.Controllers
{
    public class TmpCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TmpCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TmpCategories
        public async Task<IActionResult> Index()
        {
            return _context.TmpCategory != null ?
                        View(await _context.TmpCategory.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.TmpCategory'  is null.");
        }

        // GET: TmpCategories/Details/5
        /*public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TmpCategory == null)
            {
                return NotFound();
            }

            var tmpCategory = await _context.TmpCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tmpCategory == null)
            {
                return NotFound();
            }

            return View(tmpCategory);
        }*/

        // GET: TmpCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TmpCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TmpCategory tmpCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tmpCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tmpCategory);
        }

        // GET: TmpCategories/Edit/5
        /*public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TmpCategory == null)
            {
                return NotFound();
            }

            var tmpCategory = await _context.TmpCategory.FindAsync(id);
            if (tmpCategory == null)
            {
                return NotFound();
            }
            return View(tmpCategory);
        }

        // POST: TmpCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TmpCategory tmpCategory)
        {
            if (id != tmpCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tmpCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TmpCategoryExists(tmpCategory.Id))
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
            return View(tmpCategory);
        }*/

        // GET: TmpCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TmpCategory == null)
            {
                return NotFound();
            }

            var tmpCategory = await _context.TmpCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tmpCategory == null)
            {
                return NotFound();
            }

            return View(tmpCategory);
        }

        // POST: TmpCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TmpCategory == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TmpCategory'  is null.");
            }
            var tmpCategory = await _context.TmpCategory.FindAsync(id);
            if (tmpCategory != null)
            {
                _context.TmpCategory.Remove(tmpCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Approve(int id)
        {
            if (_context.TmpCategory == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TmpCategory' is null");
            }
            var TmpCategory = await _context.TmpCategory.FindAsync(id);
            await _context.Category.AddAsync(new Category(TmpCategory));
            if (TmpCategory != null)
            {
                _context.TmpCategory.Remove(TmpCategory);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TmpCategoryExists(int id)
        {
            return (_context.TmpCategory?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

}
