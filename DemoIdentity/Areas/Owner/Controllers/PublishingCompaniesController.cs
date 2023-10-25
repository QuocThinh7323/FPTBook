using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FPTBook.Models;
using FPTBookDemo.Data;
using Microsoft.AspNetCore.Authorization;

namespace FPTBook.Areas.Owner.Controllers
{
    [Authorize(Roles = "Owner")]
    [Area("Owner")]
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
            return _context.PublishingCompany != null ?
                        View(await _context.PublishingCompany.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.PublishingCompany'  is null.");
        }

        // GET: PublishingCompanies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PublishingCompany == null)
            {
                return NotFound();
            }

            var publishingCompany = await _context.PublishingCompany
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publishingCompany == null)
            {
                return NotFound();
            }

            return View(publishingCompany);
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
        public async Task<IActionResult> Create([Bind("Id,Name")] PublishingCompany publishingCompany)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publishingCompany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publishingCompany);
        }

        // GET: PublishingCompanies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PublishingCompany == null)
            {
                return NotFound();
            }

            var publishingCompany = await _context.PublishingCompany.FindAsync(id);
            if (publishingCompany == null)
            {
                return NotFound();
            }
            return View(publishingCompany);
        }

        // POST: PublishingCompanies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PublishingCompany publishingCompany)
        {
            if (id != publishingCompany.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publishingCompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublishingCompanyExists(publishingCompany.Id))
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
            return View(publishingCompany);
        }

        // GET: PublishingCompanies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PublishingCompany == null)
            {
                return NotFound();
            }

            var publishingCompany = await _context.PublishingCompany
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publishingCompany == null)
            {
                return NotFound();
            }

            return View(publishingCompany);
        }

        // POST: PublishingCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PublishingCompany == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PublishingCompany'  is null.");
            }
            var publishingCompany = await _context.PublishingCompany.FindAsync(id);
            if (publishingCompany != null)
            {
                _context.PublishingCompany.Remove(publishingCompany);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublishingCompanyExists(int id)
        {
            return (_context.PublishingCompany?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
