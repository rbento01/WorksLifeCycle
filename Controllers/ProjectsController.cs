using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using Works_Life_Cycle.Data;
using Works_Life_Cycle.Models;

namespace Works_Life_Cycle.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly WorksLifeCycleDB _context;

        /// <summary>
        /// objeto para gerir os dados dos Utilizadores registados
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;

        private readonly IWebHostEnvironment _webhost;

        public ProjectsController(WorksLifeCycleDB context, UserManager<IdentityUser> userManager, IWebHostEnvironment webhost)
        {
            _context = context;
            _userManager = userManager;
            _webhost = webhost;
        }
        [Authorize]
        // GET: Projects
        public async Task<IActionResult> Index()
        {

            //teacher q está logged in
            var teacherLoggedIn = await _context.Teachers.FirstOrDefaultAsync(m => m.UserNameID == _userManager.GetUserId(User));
            //estudante que está logged in
            var studentLoggedIn = await _context.Students.FirstOrDefaultAsync(m => m.UserNameID == _userManager.GetUserId(User));
            //se é estudante
            if (studentLoggedIn != null)
            {   
                var worksLifeCycleDB1 = _context.Projects.Include(p => p.AccessType)
                                                    .Include(p => p.Collection)
                                                    .Include(p => p.SchoolYear)
                                                    .Include(p => p.Language)
                                                    .Include(p => p.Course)
                                                    .Where(p => p.CourseFK == studentLoggedIn.CourseFK);
                return View(await worksLifeCycleDB1.ToListAsync());
            }
            else
            {
                var hasCandidates = await _context.ProjectStudents.ToListAsync();
                ViewBag.Candidates = hasCandidates;
                var teacherInProject = await _context.Projects.Where(m => m.ListofTeachers.Contains(teacherLoggedIn)).ToListAsync();
                ViewBag.Teachers = teacherInProject;
            }
            var worksLifeCycleDB = _context.Projects.Include(p => p.AccessType)
                                                    .Include(p => p.Collection)
                                                    .Include(p => p.SchoolYear)
                                                    .Include(p => p.Language)
                                                    .Include(p => p.Course);
            return View(await worksLifeCycleDB.ToListAsync());


        }

        [Authorize]
        // GET: Os meus Projetos
        public async Task<IActionResult> IndexU()
        {
            //Verificar se o utilizador loggedin é estudante ou professor
            //Se for professor, procura todos os projetos cujo professor é orientador
            //teacher q está logged in
            var teacherLoggedIn = await _context.Teachers.FirstOrDefaultAsync(m => m.UserNameID == _userManager.GetUserId(User));
            //estudante que está logged in
            var studentLoggedIn = await _context.Students.FirstOrDefaultAsync(m => m.UserNameID == _userManager.GetUserId(User));

            // se for professor
            if (teacherLoggedIn != null)
            {
                var orientadores = _context.Projects.Include(p => p.AccessType)
                                                 .Include(p => p.Collection)
                                                 .Include(p => p.SchoolYear)
                                                 .Include(p => p.Language)
                                                 .Include(p => p.Course)
                                                 .Where(m => m.ListofTeachers.Contains(teacherLoggedIn));
                //e se tiver projetos
                if (orientadores != null)
                {
                    //Vai buscar todos os projetos submetidos
                    var projectsGraded = await _context.Projects.Where(p => p.Grade != null && p.ListofTeachers.Contains(teacherLoggedIn)).ToListAsync();
                    ViewBag.Graded = projectsGraded;
                    //Se já tiver projetos submetidos prontos para dar nota
                    var projectsSubmmitedNoGrade = await _context.Projects.Where(p => p.InternalID != null && p.Grade == null).ToListAsync();
                    ViewBag.Projects = projectsSubmmitedNoGrade;
                    var hasCandidates = await _context.ProjectStudents.ToListAsync();
                    ViewBag.Candidates = hasCandidates;
                    return View(await orientadores.ToListAsync());
                }
                //se não
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            //se for aluno
            if (studentLoggedIn != null)
            {
                //Vai buscar todos os projetos submetidos
                var projectsSubmitted = await _context.Projects.FirstOrDefaultAsync(p => p.TID != null && p.StudentFK == studentLoggedIn.Id);
                if (projectsSubmitted == null)
                {
                    ViewBag.Submitted = false;
                }
                else
                {
                    ViewBag.Submitted = true;
                }

                //se tem projeto
                var hasProject = await _context.ProjectStudents.FirstOrDefaultAsync(m => m.StudentId == studentLoggedIn.Id && m.AcceptanceStatus == true);
                //e se tiver projeto
                if (hasProject != null)
                {
                    var worksLifeCycleDB = _context.Projects.Include(p => p.AccessType)
                                                        .Include(p => p.Collection)
                                                        .Include(p => p.SchoolYear)
                                                        .Include(p => p.Language)
                                                        .Include(p => p.Course)
                                                        .Where(p => p.ProjectId == hasProject.ProjectId);
                    return View(await worksLifeCycleDB.ToListAsync());

                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return RedirectToAction(nameof(Index));
        }
        //Nao sei se esta sintaxe funciona ainda
        [Authorize(Roles = "Teacher,Secretary")]
        // GET: Groups Candidatos
        public async Task<IActionResult> Candidates(int? id)
        {
            var Candidates = _context.ProjectStudents.Where(c => c.ProjectId == id);
            ViewBag.Candidates = Candidates.ToList();

            //ve o professor que esta loggado
            var teacherLoggedIn = await _context.Teachers.FirstOrDefaultAsync(m => m.UserNameID == _userManager.GetUserId(User));
            //busca todos os projetos que o professor esta
            var projetos = _context.Projects.Where(m => m.ListofTeachers.Contains(teacherLoggedIn));

            ViewBag.Orientador = false;

            //Verificar se o projeto já tem alunos atribuidos, para não atribuir 2 pessoas o mesmo projeto
            var projs = await _context.ProjectStudents.FirstOrDefaultAsync(p => p.ProjectId == id && p.AcceptanceStatus == true);
            //Se pessoas já tem o projeto
            if (projs != null)
            {
                ViewBag.Accept = false;
            }
            else
            {
                ViewBag.Accept = true;
            }

            //percorrer projetos e verificar se algum dos projetos do professor corresponde ao id do projeto em questao
            //se sim passa uma viewbag = true para podermos mostrar a lista ou mostrar os botoes
            foreach (var proj in projetos)
            {
                if (proj.ProjectId == id)
                {
                    ViewBag.Orientador = true;
                }
            }

            var worksLifeCycleDB = _context.ProjectStudents.Where(p => p.ProjectId == id);
            return View(await worksLifeCycleDB.ToListAsync());
        }

        [Authorize]
        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var Files = _context.Files.Where(s => s.ProjectFK == id);
            ViewBag.Files = Files;

            //teacher q está logged in
            var teacherLoggedIn = await _context.Teachers.FirstOrDefaultAsync(m => m.UserNameID == _userManager.GetUserId(User));
            //estudante que está logged in
            var studentLoggedIn = await _context.Students.FirstOrDefaultAsync(m => m.UserNameID == _userManager.GetUserId(User));

            //Se um grupo já tem este projeto
            var otherStudents = await _context.ProjectStudents.FirstOrDefaultAsync(m => m.ProjectId == id && m.AcceptanceStatus == true);

            //Se é um student
            if (studentLoggedIn != null){
                //se alguém já tme este projeto
                var hasStudentOnProject = await _context.ProjectStudents.FirstOrDefaultAsync(s => s.ProjectId == id && s.AcceptanceStatus == true);
                //se o aluno logged in já se candidatou a este projeto
                var studentApplied = await _context.ProjectStudents.FirstOrDefaultAsync(s => s.ProjectId == id && s.StudentId == studentLoggedIn.Id && s.AcceptanceStatus == false);
                //se já está atribuido noutro projeto
                var studentHasProject = await _context.ProjectStudents.FirstOrDefaultAsync(s => s.StudentId == studentLoggedIn.Id && s.AcceptanceStatus == true);
                if (hasStudentOnProject != null || studentApplied != null || studentHasProject != null)
                {
                    ViewBag.Apply = false;
                }
                else{
                    ViewBag.Apply = true;
                }
            }
            else{
                var teacherInProject = await _context.Projects.Where(m => m.ListofTeachers.Contains(teacherLoggedIn) && m.ProjectId == id).ToListAsync();
                if (teacherInProject.Count() == 0)
                {
                    ViewBag.Edit = false;
                }
                else
                {
                    ViewBag.Edit = true;
                }

                ViewBag.Apply = false;
            }

            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }
            var projectSubmitted = await _context.ProjectStudents.FirstOrDefaultAsync(p => p.ProjectId == id && p.AcceptanceStatus == true);
            var projectGraded = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id && p.Grade != null);
            if (projectSubmitted != null)
            {
                ViewBag.Submmited = true;
            }
            else
            {
                ViewBag.Submmited = false;
            }
            if(projectGraded != null)
            {
                ViewBag.Graded = true;
            }
            else
            {
                ViewBag.Graded = false;
            }
            var project = await _context.Projects
                .Include(p => p.AccessType)
                .Include(p => p.Collection)
                .Include(p => p.SchoolYear)
                .Include(p => p.Language)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }
        [Authorize(Roles = "Teacher")]
        // GET: Projects/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["AccessTypeFK"] = new SelectList(_context.AccessTypes, "AccessTypeId", "Name");
            ViewData["CourseFK"] = new SelectList(_context.Courses, "CourseID", "Name");
            ViewData["CollectionFK"] = new SelectList(_context.Collections, "CollectionID", "Name");
            ViewData["SchoolYearFK"] = new SelectList(_context.SchoolYears, "SchoolYearId", "Name");
            ViewData["LanguageFK"] = new SelectList(_context.Languages, "LanguageID", "Name");
            ViewData["LicenseFK"] = new SelectList(_context.Licenses, "LicenseId", "LicenseName");
            ViewData["TeacherFK"] = new SelectList(_context.Teachers, "Id", "Name");

            var teacherLoggedIn = await _context.Teachers.FirstOrDefaultAsync(m => m.UserNameID == _userManager.GetUserId(User));

            ViewData["TeacherFK"] = new SelectList(_context.Teachers.Where(s => s.Id != teacherLoggedIn.Id), "Id", "Name");

            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Create([Bind("ProjectId,Title,InternalID,AbstractPT,AbstractEN,Comments,Status,TID,Handle,DefenceDate,License,Sponsorships,LanguageFK,CollectionFK,AccessTypeFK,EpocaFK")] Project project, List<IFormFile> file, int language, int collection, int accessType, int epoca, int Orientadores, int Course)
        {
            if (collection == null || epoca == null || Orientadores == null || project.Title == null || project.DefenceDate == null || project.Files == null || Course == null)
            {
                ModelState.AddModelError("", "Escrever toda a informação");
                return View(project);
            }

            //@Rodrigo na minha versao do create o acesstype e language não sao especificados no momento do create daí não se passar essa info e termos q dar aqui um valor
            //project.AccessTypeFK = accessType;
            project.AccessTypeFK = 1;
            project.CollectionFK = collection;
            project.SchoolYearFK = epoca;
            project.CourseFK = Course;
            //project.LanguageFK = language;
            project.LanguageFK = 1;
            project.Status = Status.NaoPublicado;
            //Tratamento dos professores
            List<Teacher> teacherList = new List<Teacher>();
            if (Orientadores != 0)
            {
                var teacherSelected = await _context.Teachers.FirstOrDefaultAsync(m => m.Id == Orientadores);
                teacherList.Add(teacherSelected);
            }
            var teacherLoggedIn = await _context.Teachers.FirstOrDefaultAsync(m => m.UserNameID == _userManager.GetUserId(User));
            teacherList.Add(teacherLoggedIn);
            project.ListofTeachers = teacherList;

            //TRATAMENTO DO(S) FICHEIRO(S)
            List<Models.File> files = new List<Models.File>();

            foreach (var file1 in file)
            {
                //Vai ter a diretoria da pasta .../wwwroot/ficheiros/...
                var saveimg = Path.Combine(_webhost.WebRootPath, "ficheiros", file1.FileName);
                using var uploadimg = new FileStream(saveimg, FileMode.Create);
                await file1.CopyToAsync(uploadimg);

                Models.File file_ = new Models.File
                {
                    Path = saveimg,
                    Name = file1.FileName,
                };
                files.Add(file_);

                _context.Files.Add(file_);
                await _context.SaveChangesAsync();
            }

            project.Files = files;

            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccessTypeFK"] = new SelectList(_context.AccessTypes, "AccessTypeId", "Name", project.AccessTypeFK);
            ViewData["CourseFK"] = new SelectList(_context.Courses, "CourseID", "Name", project.CourseFK);
            ViewData["CollectionFK"] = new SelectList(_context.Collections, "CollectionID", "Name", project.CollectionFK);
            ViewData["SchoolYearFK"] = new SelectList(_context.SchoolYears, "SchoolYearId", "Name", project.SchoolYearFK);
            ViewData["LanguageFK"] = new SelectList(_context.Languages, "LanguageID", "Name", project.LanguageFK);
            ViewData["LicenseFK"] = new SelectList(_context.Licenses, "LicenseId", "LicenseName", project.LicenseFK);
            ViewData["TeacherFK"] = new SelectList(_context.Teachers, "Id", "Name", project.ListofTeachers);
            return View(project);
        }

        [Authorize(Roles = "Teacher,Secretary")]
        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var project = await _context.Projects.FindAsync(id);
            var teacherLoggedIn = await _context.Teachers.FirstOrDefaultAsync(m => m.UserNameID == _userManager.GetUserId(User));
            var projectHasGroup = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);
            if (teacherLoggedIn != null)
            {
                var teacherInProject = await _context.Projects.Where(m => m.ListofTeachers.Contains(teacherLoggedIn) && m.ProjectId == id).ToListAsync();
                if (teacherInProject.Count().Equals(0))
                {
                    string returnURL = "~/Identity/Account/AccessDenied";
                    return LocalRedirect(returnURL);
                }
            }
            

            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var projects = await _context.Projects.FirstOrDefaultAsync(p => p.InternalID != null && p.ProjectId == id);

            var isSubmmited = await _context.ProjectStudents.FirstOrDefaultAsync(p => p.ProjectId == id && p.AcceptanceStatus == true);
            if (isSubmmited != null)
            {
                projectHasGroup = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == isSubmmited.ProjectId && p.InternalID == null && p.ProjectId == id && p.StudentFK != null);
            }


            //Não submeteu, e tem grupo já
            //var isEditable = await _context.Projects.FirstOrDefaultAsync(p => p.InternalID == null && p.ProjectId == id && p.GroupFK != null);
            //Não tem o projeto entregue
            if (projects == null)
            {
                ViewBag.Projects = false;
            }
            else
            {
                ViewBag.Projects = true;
            }
            if (isSubmmited != null)
            {
                ViewBag.Unsubmmited = false;
            }
            else
            {
                ViewBag.Unsubmmited = true;
            }
            if (projectHasGroup != null)
            {
                ViewBag.Unsubmmited = true;
            }

            //if (isEditable != null)
            //{
            //    ViewBag.Editable = false;
            //}
            //else
            //{
            //    ViewBag.Editable = true;
            //}
            if (project == null)
            {
                return NotFound();
            }
            ViewData["AccessTypeFK"] = new SelectList(_context.AccessTypes, "AccessTypeId", "Name", project.AccessTypeFK);
            ViewData["CourseFK"] = new SelectList(_context.Courses, "CourseID", "Name", project.CourseFK);
            ViewData["CollectionFK"] = new SelectList(_context.Collections, "CollectionID", "Name", project.CollectionFK);
            ViewData["SchoolYearFK"] = new SelectList(_context.SchoolYears, "SchoolYearId", "Name", project.SchoolYearFK);
            ViewData["LanguageFK"] = new SelectList(_context.Languages, "LanguageID", "Name", project.LanguageFK);
            ViewData["LicenseFK"] = new SelectList(_context.Licenses, "LicenseId", "LicenseName", project.LicenseFK);
            ViewData["TeacherFK"] = new SelectList(_context.Teachers.Where(s => s.Id != teacherLoggedIn.Id), "Id", "Name");
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,Title,InternalID,AbstractPT,AbstractEN,Comments,Status,TID,Handle,DefenceDate,LicenseFK,Sponsorships,LanguageFK,CollectionFK,AccessTypeFK,SchoolYearFK, StudentFK, Grade,CourseFK")] Project project, int epoca, int Orientadores, int Course, int Collection)
        {
            if (!(epoca == 0 || Course == 0 || Collection == 0))
            {
                project.SchoolYearFK = epoca;
                project.CollectionFK = Collection;
                project.CourseFK = Course;
            }

            //Tratamento dos professores
            List<Teacher> teacherList = new List<Teacher>();
            if (Orientadores != 0)
            {
                var teacherSelected = await _context.Teachers.FirstOrDefaultAsync(m => m.Id == Orientadores);
                teacherList.Add(teacherSelected);
            }
            project.ListofTeachers = teacherList;

            if (id != project.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
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
            ViewData["AccessTypeFK"] = new SelectList(_context.AccessTypes, "AccessTypeId", "Name", project.AccessTypeFK);
            ViewData["CourseFK"] = new SelectList(_context.Courses, "CourseID", "Name", project.CourseFK);
            ViewData["CollectionFK"] = new SelectList(_context.Collections, "CollectionID", "Name", project.CollectionFK);
            ViewData["SchoolYearFK"] = new SelectList(_context.SchoolYears, "SchoolYearId", "Name", project.SchoolYearFK);
            ViewData["LanguageFK"] = new SelectList(_context.Languages, "LanguageID", "Name", project.LanguageFK);
            ViewData["LicenseFK"] = new SelectList(_context.Licenses, "LicenseId", "LicenseName", project.LicenseFK);
            ViewData["TeacherFK"] = new SelectList(_context.Teachers, "Id", "Name", project.ListofTeachers);
            return View(project);
        }


        //o submit é um copy paste do edit de momento , com o nome das actions mudado
        [Authorize]
        // GET: Projects/Submit/5
        public async Task<IActionResult> Submit(int? id)
        {
            var project = await _context.Projects
                .Include(p => p.AccessType)
                .Include(p => p.Collection)
                .Include(p => p.SchoolYear)
                .Include(p => p.Language)
                .FirstOrDefaultAsync(m => m.ProjectId == id);

            if (project == null)
            {
                return NotFound();
            }
            //ViewData["AccessTypeFK"] = new SelectList(_context.AccessTypes, "AccessTypeId", "Name");
            //ViewData["CollectionFK"] = new SelectList(_context.Collections, "CollectionID", "Name");
            //ViewData["SchoolYearFK"] = new SelectList(_context.SchoolYears, "SchoolYearId", "Name");
            //ViewData["LanguageFK"] = new SelectList(_context.Languages, "LanguageID", "Name");
            //ViewData["LicenseFK"] = new SelectList(_context.Licenses, "LicenseId", "LicenseName");

            ViewData["AccessTypeFK"] = new SelectList(_context.AccessTypes, "AccessTypeId", "Name");
            ViewData["CourseFK"] = new SelectList(_context.Courses, "CourseID", "Name", project.CourseFK);
            ViewData["CollectionFK"] = new SelectList(_context.Collections, "CollectionID", "Name");
            ViewData["SchoolYearFK"] = new SelectList(_context.SchoolYears, "SchoolYearId", "Name");
            ViewData["LanguageFK"] = new SelectList(_context.Languages, "LanguageID", "Name");
            ViewData["LicenseFK"] = new SelectList(_context.Licenses, "LicenseId", "LicenseName");
            ViewData["TeacherFK"] = new SelectList(_context.Teachers, "Id", "Name");
            return View(project);
        }

        // POST: Projects/Submit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Submit(int ProjectId, [Bind("ProjectId,Title,InternalID,AbstractPT,AbstractEN,Comments,Status,TID,Handle,DefenceDate,License,Sponsorships,LanguageFK,StudentFK,CollectionFK,AccessTypeFK,SchoolYearFK,CourseFK")] Project project, List<IFormFile> file, int License, int Language)
        {
            //estudante que está logged in
            var studentLoggedIn = await _context.Students.FirstOrDefaultAsync(m => m.UserNameID == _userManager.GetUserId(User));
            //ver o grupo da pessoa
            //var group = await _context.Groups.FirstOrDefaultAsync(m => m.GroupId == studentLoggedIn.GroupFK);

            //vê se o grupo da pessoa logged in é a pessoa do projeto
            var isInThisProject = await _context.ProjectStudents.FirstOrDefaultAsync(m => m.StudentId == studentLoggedIn.Id && m.ProjectId == project.ProjectId);
            project.Status = Status.Submetido;
            if (isInThisProject == null)
            {
                ModelState.AddModelError("", "This isn't your project");
            }

            if (project.InternalID == null || project.TID == null || project.AbstractPT == null || project.AbstractEN == null || project.Comments == null || License == null || Language == null || project.Status == null || file == null)
            {
                ModelState.AddModelError("", "Escrever toda a informação");
            }

            //TRATAMENTO DO(S) FICHEIRO(S)
            List<Models.File> files = new List<Models.File>();
            foreach (var file1 in file)
            {
                //Vai ter a diretoria da pasta .../wwwroot/ficheiros/...
                var saveimg = Path.Combine(_webhost.WebRootPath, "ficheiros", file1.FileName);
                using var uploadimg = new FileStream(saveimg, FileMode.Create);
                await file1.CopyToAsync(uploadimg);

                Models.File file_ = new Models.File
                {
                    Path = saveimg,
                    Name = file1.FileName,
                };
                files.Add(file_);

                _context.Files.Add(file_);
                await _context.SaveChangesAsync();
            }
            project.Files = files;
            project.LicenseFK = License;
            project.LanguageFK = Language;


            if (ProjectId != project.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Projects.RemoveRange();
                    _context.Projects.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
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
            ViewData["AccessTypeFK"] = new SelectList(_context.AccessTypes, "AccessTypeId", "Name", project.AccessTypeFK);
            ViewData["CollectionFK"] = new SelectList(_context.Collections, "CollectionID", "Name", project.CollectionFK);
            ViewData["SchoolYearFK"] = new SelectList(_context.SchoolYears, "SchoolYearId", "Name", project.SchoolYearFK);
            ViewData["LanguageFK"] = new SelectList(_context.Languages, "LanguageID", "Name", project.LanguageFK);
            ViewData["LicenseFK"] = new SelectList(_context.Licenses, "LicenseId", "LicenseName", project.LicenseFK);
            return View(project);
        }

        // POST: Projects/Submit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Apply(int ProjectId)
        {
            //Vai ter acesso ao aluno que se está a tentar candidatar ao projeto
            var StudentApplying = await _context.Students.FirstOrDefaultAsync(m => m.UserNameID == _userManager.GetUserId(User));
            //Projeto que se está a tentar candidatar
            var project = await _context.Projects.FirstOrDefaultAsync(m => m.ProjectId == ProjectId);

            //Cria-se um objeto
            ProjectStudent projectStudent = new ProjectStudent();
            //O projectId é do enviado em pârametro
            projectStudent.ProjectId = ProjectId;
            //O estado fica a falso visto que ainda não foi aceite
            projectStudent.AcceptanceStatus = false;
            //O grupo é o do aluno que se candidatou
            projectStudent.StudentId = (int)StudentApplying.Id;
            //name of the student
            projectStudent.Name = StudentApplying.StudentId + " - " + StudentApplying.Name;

            //adiciona ao contexto o objeto criado
            _context.ProjectStudents.Add(projectStudent);

            //guarda na base de dados
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Projects");
        }

        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Accept(int ProjectId, int StudentId)
        {
            //Vai-se ter que alterar os valores das tabelas: 
            // -Student.dbo DONE
            // -Project.dbo DONE
            // -ProjectGroup.dbo DONE

            //Grupo que foi aceite
            //var group = await _context.Groups.FirstOrDefaultAsync(m => m.GroupId == GroupId);
            //group.ProjectFK = ProjectId;

            //aluno que foi aceite
            var StudentApplying = await _context.Students.FirstOrDefaultAsync(m => m.Id == StudentId);
            StudentApplying.ProjectFK = ProjectId;

            //Projeto que já tem um grupo
            var project = await _context.Projects.FirstOrDefaultAsync(m => m.ProjectId == ProjectId);
            project.StudentFK = StudentApplying.Id;

            //Projeto ligado com o grupo
            var projectStudent = await _context.ProjectStudents.FirstOrDefaultAsync(m => m.StudentId == StudentApplying.Id && m.ProjectId == ProjectId);
            projectStudent.AcceptanceStatus = true;

            //Depois do grupo ser aceite, tem que se apagar todas as outras candidaturas a outros projetos, para este grupo não ficar com 2 projetos
            var removeProjectGroup = await _context.ProjectStudents.Where(m => m.StudentId == StudentApplying.Id && m.ProjectId != ProjectId).ToListAsync();

            //Outros candidatos do projeto são apagados depois de um grupo ser aceite
            var otherCandidates = await _context.ProjectStudents.Where(m => m.ProjectId == ProjectId && m.StudentId != StudentApplying.Id).ToListAsync();

            _context.Students.Update(StudentApplying);
            _context.ProjectStudents.Update(projectStudent);
            _context.Projects.Update(project);
            _context.ProjectStudents.RemoveRange(removeProjectGroup);
            _context.ProjectStudents.RemoveRange(otherCandidates);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Projects", new { id = ProjectId });
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }
            //Se este projeto tem um grupo atribuido
            var isOnProject = await _context.ProjectStudents.FirstOrDefaultAsync(p => p.ProjectId == id && p.AcceptanceStatus == true);
            if (isOnProject != null)
            {
                ViewBag.hasStudents = true;
            }
            else
            {
                ViewBag.hasStudents = false;
            }
            var project = await _context.Projects
                .Include(p => p.AccessType)
                .Include(p => p.Collection)
                .Include(p => p.SchoolYear)
                .Include(p => p.Language)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }
        [Authorize(Roles = "Teacher,Secretary")]
        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'WorksLifeCycleDB.Projects'  is null.");
            }
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                var files = await _context.Files.Where(f => f.ProjectFK == project.ProjectId).ToListAsync();
                foreach (var file in files)
                {
                    _context.Files.Remove(file);
                }
                var isAppliedProject = await _context.ProjectStudents.Where(p => p.ProjectId == id).ToListAsync();
                if (isAppliedProject != null)
                {
                    foreach (var group in isAppliedProject)
                    {
                        _context.ProjectStudents.Remove(group);
                    }
                }
                _context.Projects.Remove(project);
            }
            //Se este projeto tem um grupo atribuido
            var isOnProject = await _context.ProjectStudents.FirstOrDefaultAsync(p => p.ProjectId == id && p.AcceptanceStatus == true);
            if (isOnProject != null)
            {

            }
            else
            {
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return (_context.Projects?.Any(e => e.ProjectId == id)).GetValueOrDefault();
        }
    }
}