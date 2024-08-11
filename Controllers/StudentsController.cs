using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Works_Life_Cycle.Data;
using Works_Life_Cycle.Models;

namespace Works_Life_Cycle.Controllers {
    public class StudentsController : Controller {
        private readonly WorksLifeCycleDB _context;

        /// <summary>
        /// objeto para gerir os dados dos Utilizadores registados
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;

        public StudentsController(WorksLifeCycleDB context, UserManager<IdentityUser> userManager) {
            _context = context;
            _userManager = userManager;
        }

        // GET: Students
        public async Task<IActionResult> Index() {
            var worksLifeCycleDB = _context.Students.Include(s => s.Nationality).Include(s=>s.Course);
            return View(await worksLifeCycleDB.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Students == null) {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Nationality)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null) {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        [Authorize(Roles = "Student")]
        [Authorize]
        public IActionResult Create() {
            ViewData["NationalityFK"] = new SelectList(_context.Nationalities, "NationalityId", "Name");
            ViewData["CourseFK"] = new SelectList(_context.Courses, "CourseID", "Name");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Student")]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,Id,UserNameID,Email,Email2,Name,Sex,BirthDate,IDNumber,IDType,NationalityFK,CourseFK,Address,Telephone,Role")] Student student) {
            if (student.StudentId == null || student.Email2 == null || student.Name == null || student.Sex == null || student.BirthDate == null || student.IDNumber == null || student.IDType == null || student.NationalityFK == null || student.Address == null || student.Telephone == null){
                ModelState.AddModelError("", "Enter ALL information");
                return View(student);
            }
            
            Person cur_person = await _context.People.FirstOrDefaultAsync(m => m.UserNameID == _userManager.GetUserId(User));

            if (cur_person != null) {
                student.UserNameID = cur_person.UserNameID;
                student.Email = cur_person.Email;
                student.Role = cur_person.Role;
            }
            _context.People.Remove(cur_person);
            _context.People.Add(student);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Students == null) {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null) {
                return NotFound();
            }
            ViewData["NationalityFK"] = new SelectList(_context.Nationalities, "NationalityId", "Name", student.NationalityFK);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,Id,UserNameID,Email,Email2,Name,Sex,BirthDate,IDNumber,IDType,ORCID,NationalityFK,Address,Telephone,Role")] Student student) {
            if (id != student.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!StudentExists(student.Id)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["NationalityFK"] = new SelectList(_context.Nationalities, "NationalityId", "Name", student.NationalityFK);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Students == null) {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Nationality)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null) {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Students == null) {
                return Problem("Entity set 'WorksLifeCycleDB.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null) {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id) {
            return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
