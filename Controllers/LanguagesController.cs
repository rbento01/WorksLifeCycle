﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Works_Life_Cycle.Data;
using Works_Life_Cycle.Models;

namespace Works_Life_Cycle.Controllers {
    [Authorize(Roles = "Admin")]
    public class LanguagesController : Controller {
        private readonly WorksLifeCycleDB _context;

        public LanguagesController(WorksLifeCycleDB context) {
            _context = context;
        }

        // GET: Languages
        public async Task<IActionResult> Index() {
            return _context.Languages != null ?
                        View(await _context.Languages.ToListAsync()) :
                        Problem("Entity set 'WorksLifeCycleDB.Languages'  is null.");
        }

        // GET: Languages/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Languages == null) {
                return NotFound();
            }

            var language = await _context.Languages
                .FirstOrDefaultAsync(m => m.LanguageID == id);
            if (language == null) {
                return NotFound();
            }

            return View(language);
        }

        // GET: Languages/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create() {
            return View();
        }

        // POST: Languages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LanguageID,Name")] Language language) {
            if (ModelState.IsValid) {
                _context.Add(language);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(language);
        }

        // GET: Languages/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Languages == null) {
                return NotFound();
            }

            var language = await _context.Languages.FindAsync(id);
            if (language == null) {
                return NotFound();
            }
            return View(language);
        }

        // POST: Languages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LanguageID,Name")] Language language) {
            if (id != language.LanguageID) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(language);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!LanguageExists(language.LanguageID)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(language);
        }

        // GET: Languages/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Languages == null) {
                return NotFound();
            }

            var language = await _context.Languages
                .FirstOrDefaultAsync(m => m.LanguageID == id);
            if (language == null) {
                return NotFound();
            }

            return View(language);
        }

        // POST: Languages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Languages == null) {
                return Problem("Entity set 'WorksLifeCycleDB.Languages'  is null.");
            }
            var language = await _context.Languages.FindAsync(id);
            if (language != null) {
                _context.Languages.Remove(language);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LanguageExists(int id) {
            return (_context.Languages?.Any(e => e.LanguageID == id)).GetValueOrDefault();
        }
    }
}
