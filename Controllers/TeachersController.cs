using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Works_Life_Cycle.Data;
using Works_Life_Cycle.Models;

namespace Works_Life_Cycle.Controllers {
    public class TeachersController : Controller {
        private readonly WorksLifeCycleDB _context;

        /// <summary>
        /// objeto para gerir os dados dos Utilizadores registados
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;

        public TeachersController(WorksLifeCycleDB context, UserManager<IdentityUser> userManager) {
            _context = context;
            _userManager = userManager;
        }

        // GET: Teachers
        public async Task<IActionResult> Index() {
            var worksLifeCycleDB = _context.Teachers.Include(t => t.Nationality);
            return View(await worksLifeCycleDB.ToListAsync());
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Teachers == null) {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .Include(t => t.Nationality)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null) {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teachers/Create
        [Authorize(Roles = "Teacher")]
        [Authorize]
        public IActionResult Create() {
            ViewData["NationalityFK"] = new SelectList(_context.Nationalities, "NationalityId", "Name");
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("External,PhD,Specialist,Id,UserNameID,Email,Email2,Name,Sex,BirthDate,IDNumber,IDType,ORCID,NationalityFK,Address,Telephone,Role")] Teacher teacher) {
            if ((teacher.External == false && teacher.PhD == false && teacher.Specialist == false) || teacher.Email2 == null || teacher.Name == null || teacher.Sex == null || teacher.BirthDate == null || teacher.IDNumber == null || teacher.IDType == null || teacher.ORCID == null || teacher.NationalityFK == null || teacher.Address == null || teacher.Telephone == null){
                ModelState.AddModelError("", "Enter ALL information");
                return View(teacher);
            }
            
            Person cur_person = await _context.People.FirstOrDefaultAsync(m => m.UserNameID == _userManager.GetUserId(User));

            if (cur_person != null) {
                teacher.UserNameID = cur_person.UserNameID;
                teacher.Email = cur_person.Email;
                teacher.Role = cur_person.Role;
            }
            _context.People.Remove(cur_person);
            _context.People.Add(teacher);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Teachers == null) {
                return NotFound();
            }

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) {
                return NotFound();
            }
            ViewData["NationalityFK"] = new SelectList(_context.Nationalities, "NationalityId", "Name", teacher.NationalityFK);
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("External,PhD,Specialist,Id,UserNameID,Email,Email2,Name,Sex,BirthDate,IDNumber,IDType,ORCID,NationalityFK,Address,Telephone,Role")] Teacher teacher) {
            if (id != teacher.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!TeacherExists(teacher.Id)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["NationalityFK"] = new SelectList(_context.Nationalities, "NationalityId", "Name", teacher.NationalityFK);
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Teachers == null) {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .Include(t => t.Nationality)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null) {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Teachers == null) {
                return Problem("Entity set 'WorksLifeCycleDB.Teachers'  is null.");
            }
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher != null) {
                _context.Teachers.Remove(teacher);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id) {
            return (_context.Teachers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
