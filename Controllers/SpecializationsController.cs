using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Works_Life_Cycle.Data;
using Works_Life_Cycle.Models;

namespace Works_Life_Cycle.Controllers {
    [Authorize(Roles = "Admin")]
    public class SpecializationsController : Controller {
        private readonly WorksLifeCycleDB _context;

        public SpecializationsController(WorksLifeCycleDB context) {
            _context = context;
        }

        // GET: Specializations
        public async Task<IActionResult> Index() {
            var worksLifeCycleDB = _context.Specializations.Include(s => s.Course);
            return View(await worksLifeCycleDB.ToListAsync());
        }

        // GET: Specializations/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Specializations == null) {
                return NotFound();
            }

            var specialization = await _context.Specializations
                .Include(s => s.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialization == null) {
                return NotFound();
            }

            return View(specialization);
        }

        // GET: Specializations/Create
        [Authorize(Roles ="Admin")]
        public IActionResult Create() {
            ViewData["CourseFK"] = new SelectList(_context.Courses, "CourseID", "Code");
            return View();
        }

        // POST: Specializations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CourseFK")] Specialization specialization) {
            if (ModelState.IsValid) {
                _context.Add(specialization);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseFK"] = new SelectList(_context.Courses, "CourseID", "Code", specialization.CourseFK);
            return View(specialization);
        }

        // GET: Specializations/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Specializations == null) {
                return NotFound();
            }

            var specialization = await _context.Specializations.FindAsync(id);
            if (specialization == null) {
                return NotFound();
            }
            ViewData["CourseFK"] = new SelectList(_context.Courses, "CourseID", "Code", specialization.CourseFK);
            return View(specialization);
        }

        // POST: Specializations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CourseFK")] Specialization specialization) {
            if (id != specialization.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(specialization);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!SpecializationExists(specialization.Id)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseFK"] = new SelectList(_context.Courses, "CourseID", "Code", specialization.CourseFK);
            return View(specialization);
        }

        // GET: Specializations/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Specializations == null) {
                return NotFound();
            }

            var specialization = await _context.Specializations
                .Include(s => s.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialization == null) {
                return NotFound();
            }

            return View(specialization);
        }

        // POST: Specializations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Specializations == null) {
                return Problem("Entity set 'WorksLifeCycleDB.Specializations'  is null.");
            }
            var specialization = await _context.Specializations.FindAsync(id);
            if (specialization != null) {
                _context.Specializations.Remove(specialization);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecializationExists(int id) {
            return (_context.Specializations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
