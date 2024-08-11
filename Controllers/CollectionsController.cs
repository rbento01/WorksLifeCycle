﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Works_Life_Cycle.Data;
using Works_Life_Cycle.Models;

namespace Works_Life_Cycle.Controllers {
    [Authorize(Roles = "Admin")]
    public class CollectionsController : Controller {
        private readonly WorksLifeCycleDB _context;

        public CollectionsController(WorksLifeCycleDB context) {
            _context = context;
        }

        // GET: Collections
        public async Task<IActionResult> Index() {
            return _context.Collections != null ?
                        View(await _context.Collections.ToListAsync()) :
                        Problem("Entity set 'WorksLifeCycleDB.Collections'  is null.");
        }

        // GET: Collections/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Collections == null) {
                return NotFound();
            }

            var collection = await _context.Collections
                .FirstOrDefaultAsync(m => m.CollectionID == id);
            if (collection == null) {
                return NotFound();
            }

            return View(collection);
        }

        // GET: Collections/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create() {
            return View();
        }

        // POST: Collections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CollectionID,Name")] Collection collection) {
            if (ModelState.IsValid) {
                _context.Add(collection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(collection);
        }

        // GET: Collections/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Collections == null) {
                return NotFound();
            }

            var collection = await _context.Collections.FindAsync(id);
            if (collection == null) {
                return NotFound();
            }
            return View(collection);
        }

        // POST: Collections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CollectionID,Name")] Collection collection) {
            if (id != collection.CollectionID) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(collection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!CollectionExists(collection.CollectionID)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(collection);
        }

        // GET: Collections/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Collections == null) {
                return NotFound();
            }

            var collection = await _context.Collections
                .FirstOrDefaultAsync(m => m.CollectionID == id);
            if (collection == null) {
                return NotFound();
            }

            return View(collection);
        }

        // POST: Collections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Collections == null) {
                return Problem("Entity set 'WorksLifeCycleDB.Collections'  is null.");
            }
            var collection = await _context.Collections.FindAsync(id);
            if (collection != null) {
                _context.Collections.Remove(collection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollectionExists(int id) {
            return (_context.Collections?.Any(e => e.CollectionID == id)).GetValueOrDefault();
        }
    }
}
