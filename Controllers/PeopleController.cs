using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Works_Life_Cycle.Data;
using Works_Life_Cycle.Models;


namespace Works_Life_Cycle.Controllers {
    [Authorize(Roles ="Secretary")]
    public class PeopleController : Controller {
        private readonly WorksLifeCycleDB _context;

        /// <summary>
        /// objeto para gerir os dados dos Utilizadores registados
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;

        public PeopleController(WorksLifeCycleDB context, UserManager<IdentityUser> userManager) {
            _context = context;
            _userManager = userManager;
        }

        // GET: People
        public async Task<IActionResult> Index() {
            var worksLifeCycleDB = _context.People.Include(s => s.Nationality);
            return View(await worksLifeCycleDB.ToListAsync());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.People == null) {
                return NotFound();
            }

            var person = await _context.People
                .Include(s => s.Nationality)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null) {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        [Authorize]
        public IActionResult Create() {
            ViewData["NationalityFK"] = new SelectList(_context.Nationalities, "NationalityId", "Name");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserNameID,Email,Email2,Name,Sex,BirthDate,IDNumber,IDType,ORCID,NationalityFK,Address,Telephone")] Person person) {

            Person cur_person = await _context.People.FirstOrDefaultAsync(m => m.UserNameID == _userManager.GetUserId(User));
            if (cur_person != null) {
                person.Id = cur_person.Id;
                person.UserNameID = cur_person.UserNameID;
                person.Email = cur_person.Email;
                person.Role = cur_person.Role;
            }
            _context.People.Remove(cur_person);
            _context.People.Add(person);
            await _context.SaveChangesAsync();

            if (person != null && person.Role != null) {
                if (person.Role.Equals("Aluno")) {
                    return RedirectToAction("Create", "Students");
                }
                else if (person.Role.Equals("Professor")) {
                    return RedirectToAction("Create", "Teachers");
                }
                else {
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.People == null) {
                return NotFound();
            }

            var person = await _context.People.FindAsync(id);
            if (person == null) {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserNameID,Email,Email2,Name,Sex,BirthDate,IDNumber,IDType,ORCID,NationalityFK,Address,Telephone")] Person person) {
            if (id != person.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!PersonExists(person.Id)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.People == null) {
                return NotFound();
            }

            var person = await _context.People
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null) {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.People == null) {
                return Problem("Entity set 'WorksLifeCycleDB.People'  is null.");
            }
            var person = await _context.People.FindAsync(id);
            if (person != null) {
                _context.People.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id) {
            return (_context.People?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}