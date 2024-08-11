using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Works_Life_Cycle.Data;
using Works_Life_Cycle.Models;

namespace Works_Life_Cycle.Controllers {
    [Authorize(Roles = "Admin")]
    public class SchoolYearsController : Controller {
        private readonly WorksLifeCycleDB _context;

        public SchoolYearsController(WorksLifeCycleDB context) {
            _context = context;
        }

        // GET: SchoolYears
        public async Task<IActionResult> Index() {
            return _context.SchoolYears != null ?
                        View(await _context.SchoolYears.ToListAsync()) :
                        Problem("Entity set 'WorksLifeCycleDB.SchoolYears'  is null.");
        }

        // GET: SchoolYears/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.SchoolYears == null) {
                return NotFound();
            }

            var schoolYear = await _context.SchoolYears
                .FirstOrDefaultAsync(m => m.SchoolYearId == id);
            if (schoolYear == null) {
                return NotFound();
            }

            return View(schoolYear);
        }

        // GET: SchoolYears/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create() {
            return View();
        }

        // POST: SchoolYears/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SchoolYearId,Name")] SchoolYear schoolYear) {
            if (ModelState.IsValid) {
                _context.Add(schoolYear);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(schoolYear);
        }

        // GET: SchoolYears/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.SchoolYears == null) {
                return NotFound();
            }

            var schoolYear = await _context.SchoolYears.FindAsync(id);
            if (schoolYear == null) {
                return NotFound();
            }
            return View(schoolYear);
        }

        // POST: SchoolYears/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SchoolYearId,Name")] SchoolYear schoolYear) {
            if (id != schoolYear.SchoolYearId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(schoolYear);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!SchoolYearExists(schoolYear.SchoolYearId)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(schoolYear);
        }

        // GET: SchoolYears/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.SchoolYears == null) {
                return NotFound();
            }

            var schoolYear = await _context.SchoolYears
                .FirstOrDefaultAsync(m => m.SchoolYearId == id);
            if (schoolYear == null) {
                return NotFound();
            }

            return View(schoolYear);
        }

        // POST: SchoolYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.SchoolYears == null) {
                return Problem("Entity set 'WorksLifeCycleDB.SchoolYears'  is null.");
            }
            var schoolYear = await _context.SchoolYears.FindAsync(id);
            if (schoolYear != null) {
                _context.SchoolYears.Remove(schoolYear);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolYearExists(int id) {
            return (_context.SchoolYears?.Any(e => e.SchoolYearId == id)).GetValueOrDefault();
        }
    }
}
