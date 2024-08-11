using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Works_Life_Cycle.Data;
using Works_Life_Cycle.Models;

namespace Works_Life_Cycle.Controllers {
    [Authorize(Roles = "Admin")]
    public class AccessTypesController : Controller {
        private readonly WorksLifeCycleDB _context;

        public AccessTypesController(WorksLifeCycleDB context) {
            _context = context;
        }
        
        // GET: AccessTypes
        public async Task<IActionResult> Index() {
            return _context.AccessTypes != null ?
                        View(await _context.AccessTypes.ToListAsync()) :
                        Problem("Entity set 'WorksLifeCycleDB.AccessTypes'  is null.");
        }

        // GET: AccessTypes/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.AccessTypes == null) {
                return NotFound();
            }

            var accessType = await _context.AccessTypes
                .FirstOrDefaultAsync(m => m.AccessTypeId == id);
            if (accessType == null) {
                return NotFound();
            }

            return View(accessType);
        }

        // GET: AccessTypes/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create() {
            return View();
        }

        // POST: AccessTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccessTypeId,Name")] AccessType accessType) {
            if (ModelState.IsValid) {
                _context.Add(accessType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accessType);
        }

        // GET: AccessTypes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.AccessTypes == null) {
                return NotFound();
            }

            var accessType = await _context.AccessTypes.FindAsync(id);
            if (accessType == null) {
                return NotFound();
            }
            return View(accessType);
        }

        // POST: AccessTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccessTypeId,Name")] AccessType accessType) {
            if (id != accessType.AccessTypeId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(accessType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!AccessTypeExists(accessType.AccessTypeId)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(accessType);
        }

        // GET: AccessTypes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.AccessTypes == null) {
                return NotFound();
            }

            var accessType = await _context.AccessTypes
                .FirstOrDefaultAsync(m => m.AccessTypeId == id);
            if (accessType == null) {
                return NotFound();
            }

            return View(accessType);
        }

        // POST: AccessTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.AccessTypes == null) {
                return Problem("Entity set 'WorksLifeCycleDB.AccessTypes'  is null.");
            }
            var accessType = await _context.AccessTypes.FindAsync(id);
            if (accessType != null) {
                _context.AccessTypes.Remove(accessType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccessTypeExists(int id) {
            return (_context.AccessTypes?.Any(e => e.AccessTypeId == id)).GetValueOrDefault();
        }
    }
}
