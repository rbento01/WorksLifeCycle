using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Works_Life_Cycle.Data;
using Works_Life_Cycle.Models;

namespace Works_Life_Cycle.Controllers {
    [Authorize(Roles = "Admin")]
    public class AreasController : Controller {
        private readonly WorksLifeCycleDB _context;

        public AreasController(WorksLifeCycleDB context) {
            _context = context;
        }

        // GET: Areas

        public async Task<IActionResult> Index() {
            return _context.Areas != null ?
                        View(await _context.Areas.ToListAsync()) :
                        Problem("Entity set 'WorksLifeCycleDB.Areas'  is null.");
        }

        // GET: Areas/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Areas == null) {
                return NotFound();
            }

            var area = await _context.Areas
                .FirstOrDefaultAsync(m => m.AreaID == id);
            if (area == null) {
                return NotFound();
            }

            return View(area);
        }

        // GET: Areas/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create() {
            return View();
        }

        // POST: Areas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AreaID,Name")] Area area) {
            if (ModelState.IsValid) {
                _context.Add(area);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(area);
        }

        // GET: Areas/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Areas == null) {
                return NotFound();
            }

            var area = await _context.Areas.FindAsync(id);
            if (area == null) {
                return NotFound();
            }
            return View(area);
        }

        // POST: Areas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AreaID,Name")] Area area) {
            if (id != area.AreaID) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(area);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!AreaExists(area.AreaID)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(area);
        }

        // GET: Areas/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Areas == null) {
                return NotFound();
            }

            var area = await _context.Areas
                .FirstOrDefaultAsync(m => m.AreaID == id);
            if (area == null) {
                return NotFound();
            }

            return View(area);
        }

        // POST: Areas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Areas == null) {
                return Problem("Entity set 'WorksLifeCycleDB.Areas'  is null.");
            }
            var area = await _context.Areas.FindAsync(id);
            if (area != null) {
                _context.Areas.Remove(area);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AreaExists(int id) {
            return (_context.Areas?.Any(e => e.AreaID == id)).GetValueOrDefault();
        }
    }
}
