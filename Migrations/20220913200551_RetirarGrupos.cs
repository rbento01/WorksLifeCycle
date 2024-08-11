using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Works_Life_Cycle.Migrations
{
    public partial class RetirarGrupos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessTypes",
                columns: table => new
                {
                    AccessTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessTypes", x => x.AccessTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    AreaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.AreaID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    CollectionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.CollectionID);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    LanguageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LanguageID);
                });

            migrationBuilder.CreateTable(
                name: "Licenses",
                columns: table => new
                {
                    LicenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenseName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licenses", x => x.LicenseId);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    NationalityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Acronym = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.NationalityId);
                });

            migrationBuilder.CreateTable(
                name: "OrganicUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Acronym = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganicUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchoolYears",
                columns: table => new
                {
                    SchoolYearId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolYears", x => x.SchoolYearId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecializationFK = table.Column<int>(type: "int", nullable: true),
                    AreaFK = table.Column<int>(type: "int", nullable: true),
                    Partnership = table.Column<bool>(type: "bit", nullable: false),
                    OrganicUnitFK = table.Column<int>(type: "int", nullable: true),
                    CollectionFK = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseID);
                    table.ForeignKey(
                        name: "FK_Courses_Areas_AreaFK",
                        column: x => x.AreaFK,
                        principalTable: "Areas",
                        principalColumn: "AreaID");
                    table.ForeignKey(
                        name: "FK_Courses_Collections_CollectionFK",
                        column: x => x.CollectionFK,
                        principalTable: "Collections",
                        principalColumn: "CollectionID");
                    table.ForeignKey(
                        name: "FK_Courses_OrganicUnits_OrganicUnitFK",
                        column: x => x.OrganicUnitFK,
                        principalTable: "OrganicUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseFK = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Specializations_Courses_CourseFK",
                        column: x => x.CourseFK,
                        principalTable: "Courses",
                        principalColumn: "CourseID");
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectFK = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.FileId);
                });

            migrationBuilder.CreateTable(
                name: "KeywordEnglish",
                columns: table => new
                {
                    KeywordEnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeywordName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectFK = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordEnglish", x => x.KeywordEnId);
                });

            migrationBuilder.CreateTable(
                name: "KeywordNative",
                columns: table => new
                {
                    KeywordNativeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeywordName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageFK = table.Column<int>(type: "int", nullable: true),
                    ProjectFK = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordNative", x => x.KeywordNativeId);
                    table.ForeignKey(
                        name: "FK_KeywordNative_Languages_LanguageFK",
                        column: x => x.LanguageFK,
                        principalTable: "Languages",
                        principalColumn: "LanguageID");
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserNameID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sex = table.Column<int>(type: "int", nullable: true),
                    BirthDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDType = table.Column<int>(type: "int", nullable: true),
                    NationalityFK = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    ProjectFK = table.Column<int>(type: "int", nullable: true),
                    CourseFK = table.Column<int>(type: "int", nullable: true),
                    External = table.Column<bool>(type: "bit", nullable: true),
                    PhD = table.Column<bool>(type: "bit", nullable: true),
                    Specialist = table.Column<bool>(type: "bit", nullable: true),
                    ORCID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_Courses_CourseFK",
                        column: x => x.CourseFK,
                        principalTable: "Courses",
                        principalColumn: "CourseID");
                    table.ForeignKey(
                        name: "FK_People_Nationalities_NationalityFK",
                        column: x => x.NationalityFK,
                        principalTable: "Nationalities",
                        principalColumn: "NationalityId");
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalID = table.Column<int>(type: "int", nullable: true),
                    AbstractPT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AbstractEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    TID = table.Column<int>(type: "int", nullable: true),
                    Handle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grade = table.Column<int>(type: "int", nullable: true),
                    DefenceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StudentFK = table.Column<int>(type: "int", nullable: true),
                    AccessTypeFK = table.Column<int>(type: "int", nullable: true),
                    SchoolYearFK = table.Column<int>(type: "int", nullable: true),
                    LanguageFK = table.Column<int>(type: "int", nullable: true),
                    CollectionFK = table.Column<int>(type: "int", nullable: true),
                    LicenseFK = table.Column<int>(type: "int", nullable: true),
                    CourseFK = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_AccessTypes_AccessTypeFK",
                        column: x => x.AccessTypeFK,
                        principalTable: "AccessTypes",
                        principalColumn: "AccessTypeId");
                    table.ForeignKey(
                        name: "FK_Projects_Collections_CollectionFK",
                        column: x => x.CollectionFK,
                        principalTable: "Collections",
                        principalColumn: "CollectionID");
                    table.ForeignKey(
                        name: "FK_Projects_Courses_CourseFK",
                        column: x => x.CourseFK,
                        principalTable: "Courses",
                        principalColumn: "CourseID");
                    table.ForeignKey(
                        name: "FK_Projects_Languages_LanguageFK",
                        column: x => x.LanguageFK,
                        principalTable: "Languages",
                        principalColumn: "LanguageID");
                    table.ForeignKey(
                        name: "FK_Projects_Licenses_LicenseFK",
                        column: x => x.LicenseFK,
                        principalTable: "Licenses",
                        principalColumn: "LicenseId");
                    table.ForeignKey(
                        name: "FK_Projects_People_StudentFK",
                        column: x => x.StudentFK,
                        principalTable: "People",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Projects_SchoolYears_SchoolYearFK",
                        column: x => x.SchoolYearFK,
                        principalTable: "SchoolYears",
                        principalColumn: "SchoolYearId");
                });

            migrationBuilder.CreateTable(
                name: "ProjectStudents",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcceptanceStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStudents", x => new { x.ProjectId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_ProjectStudents_People_StudentId",
                        column: x => x.StudentId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectStudents_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTeacher",
                columns: table => new
                {
                    ListofProjectsProjectId = table.Column<int>(type: "int", nullable: false),
                    ListofTeachersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTeacher", x => new { x.ListofProjectsProjectId, x.ListofTeachersId });
                    table.ForeignKey(
                        name: "FK_ProjectTeacher_People_ListofTeachersId",
                        column: x => x.ListofTeachersId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTeacher_Projects_ListofProjectsProjectId",
                        column: x => x.ListofProjectsProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResearchProjects",
                columns: table => new
                {
                    ResearchProjectsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchProjects", x => x.ResearchProjectsID);
                    table.ForeignKey(
                        name: "FK_ResearchProjects_Projects_ProjectFK",
                        column: x => x.ProjectFK,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccessTypes",
                columns: new[] { "AccessTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "Acesso livre" },
                    { 2, "Acesso Embargado(1 ano)" },
                    { 3, "Acesso Embargado(2 ano)" }
                });

            migrationBuilder.InsertData(
                table: "Areas",
                columns: new[] { "AreaID", "Name" },
                values: new object[,]
                {
                    { 1, "Ciências Informáticas" },
                    { 2, "História e Arqueologia" },
                    { 3, "Finanças, Banca e Seguros" },
                    { 4, "Artesanato" },
                    { 5, "Turismo e Lazer" },
                    { 6, "Design" },
                    { 7, "Electrónica e Automação" },
                    { 8, "Ciências Informáticas" },
                    { 9, "Metalurgia e Metalomecânica" },
                    { 10, "Gestão e Administração" },
                    { 11, "Arquitectura e Urbanismo" },
                    { 12, "Tecnologia dos Processos Químicos" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a", "c3aab8a5-c3f5-463e-b4f4-51484978ba0b", "Admin", "ADMIN" },
                    { "c", "d7d3c06a-8a44-4ce1-8be4-0eeab1d15d52", "Secretary", "SECRETARY" },
                    { "s", "ba77bd35-590b-4a43-879a-86506073dc74", "Student", "STUDENT" },
                    { "t", "9729d5c1-6032-414a-affc-54cb8c2f3426", "Teacher", "TEACHER" }
                });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionID", "Name" },
                values: new object[,]
                {
                    { 1, "IPT - ESTT - Dissertações de Mestrado ou Doutoramento" },
                    { 2, "IPT - ESGT - Dissertações de Mestrado ou Doutoramento" },
                    { 3, "IPT - ESTA - Dissertações de Mestrado ou Doutoramento" },
                    { 4, "IPT - CGeo - Dissertações de Mestrado" },
                    { 5, "IPT - Ci2 - Dissertações de Mestrado" },
                    { 6, "IPT - Techn&Art - Dissertações de Mestrado" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseID", "AreaFK", "Code", "CollectionFK", "Name", "OrganicUnitFK", "Partnership", "SpecializationFK" },
                values: new object[,]
                {
                    { 1, null, "M932", null, "Analítica e Inteligência Organizacional", null, false, null },
                    { 2, null, "6498", null, "Arqueologia Pré-Histórica e Arte Rupestre", null, false, null },
                    { 3, null, "M925", null, "Auditoria e Finanças", null, false, null },
                    { 4, null, "MB41", null, "Avaliação e Gestão de Ativos Imobiliários", null, false, null },
                    { 5, null, "9405", null, "Conservação e Restauro", null, false, null },
                    { 6, null, "M013", null, "Desenvolvimento de Produtos de Turismo Cultural", null, false, null },
                    { 7, null, "M474", null, "Design Editorial", null, false, null },
                    { 8, null, "M778", null, "Engenharia Eletrotécnica", null, false, null },
                    { 9, null, "M909", null, "Engenharia Informática-Internet das Coisas", null, false, null },
                    { 10, null, "M746", null, "Engenharia Mecânica - Projecto e Produção Mecânica", null, false, null },
                    { 11, null, "9299", null, "Gestão de Recursos Humanos", null, false, null },
                    { 12, null, "9295", null, "Gestão", null, false, null },
                    { 13, null, "M199", null, "Reabilitação Urbana", null, false, null },
                    { 14, null, "M079", null, "Tecnologia Química", null, false, null },
                    { 15, null, "M197", null, "Técnicas de Arqueologia", null, false, null }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "LanguageID", "Name" },
                values: new object[,]
                {
                    { 1, "Português" },
                    { 2, "Inglês" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "LanguageID", "Name" },
                values: new object[,]
                {
                    { 3, "Espanhol" },
                    { 4, "Alemão" },
                    { 5, "Francês" },
                    { 6, "Italiano" },
                    { 7, "Mirandês" },
                    { 8, "Japonês" },
                    { 9, "Chinês" },
                    { 10, "Outro" },
                    { 11, "N/A" }
                });

            migrationBuilder.InsertData(
                table: "Licenses",
                columns: new[] { "LicenseId", "LicenseName" },
                values: new object[,]
                {
                    { 1, "Sem Licença" },
                    { 2, "Atribuição (CC-BY)" },
                    { 3, "Atribuição, Sem Trabalhos Derivados (CC-BY-ND)" },
                    { 4, "Atribuição, Partilha nos Termos da Mesma Licença (CC-BY-SA)" },
                    { 5, "Atribuição, Uso Não Comercial (CC-BY-NC)" },
                    { 6, "Atribuição, Não Comercial, Sem Derivações (CC-BY-NC-ND)" },
                    { 7, "Atribuição, Uso Não Comercial, Partilha nos Termos da Mesma Licença(CC - BY - NC - SA)" }
                });

            migrationBuilder.InsertData(
                table: "Nationalities",
                columns: new[] { "NationalityId", "Acronym", "Name" },
                values: new object[,]
                {
                    { 1, "PT", "Portuguesa" },
                    { 2, "ES", "Espanhola" },
                    { 3, "UK", "Britânica" },
                    { 4, "FR", "Francesa" },
                    { 5, "DE", "Alemã" },
                    { 6, "NL", "Holandesa" },
                    { 7, "CH", "Suiça" },
                    { 8, "CZ", "Checa" },
                    { 9, "PL", "Polaca" },
                    { 10, "DK", "Dinamarquesa" },
                    { 11, "SE", "Sueca" },
                    { 12, "TR", "Turca" },
                    { 13, "IT", "Italiana" },
                    { 14, "US", "Americana" },
                    { 15, "RU", "Russa" },
                    { 16, "XX", "Outra" }
                });

            migrationBuilder.InsertData(
                table: "OrganicUnits",
                columns: new[] { "Id", "Acronym", "Name" },
                values: new object[,]
                {
                    { 1, "ESTT", "Escola Superior de Tecnologia de Tomar" },
                    { 2, "ESGT", "Escola Superior de Gestão de Tomar" },
                    { 3, "ESTA", "Escola Superior de Tecnologia de Abrantes" }
                });

            migrationBuilder.InsertData(
                table: "SchoolYears",
                columns: new[] { "SchoolYearId", "Name" },
                values: new object[,]
                {
                    { 1, "2019/2020" },
                    { 2, "2020/2021" },
                    { 3, "2021/2022" }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "CourseFK", "Name" },
                values: new object[,]
                {
                    { 1, null, "Sem especialização" },
                    { 2, null, "Área de especialização: Gestão do Património Cultural" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AreaFK",
                table: "Courses",
                column: "AreaFK");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CollectionFK",
                table: "Courses",
                column: "CollectionFK");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_OrganicUnitFK",
                table: "Courses",
                column: "OrganicUnitFK");

            migrationBuilder.CreateIndex(
                name: "IX_Files_ProjectFK",
                table: "Files",
                column: "ProjectFK");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordEnglish_ProjectFK",
                table: "KeywordEnglish",
                column: "ProjectFK");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordNative_LanguageFK",
                table: "KeywordNative",
                column: "LanguageFK");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordNative_ProjectFK",
                table: "KeywordNative",
                column: "ProjectFK");

            migrationBuilder.CreateIndex(
                name: "IX_People_CourseFK",
                table: "People",
                column: "CourseFK");

            migrationBuilder.CreateIndex(
                name: "IX_People_NationalityFK",
                table: "People",
                column: "NationalityFK");

            migrationBuilder.CreateIndex(
                name: "IX_People_ProjectFK",
                table: "People",
                column: "ProjectFK");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AccessTypeFK",
                table: "Projects",
                column: "AccessTypeFK");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CollectionFK",
                table: "Projects",
                column: "CollectionFK");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CourseFK",
                table: "Projects",
                column: "CourseFK");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_LanguageFK",
                table: "Projects",
                column: "LanguageFK");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_LicenseFK",
                table: "Projects",
                column: "LicenseFK");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_SchoolYearFK",
                table: "Projects",
                column: "SchoolYearFK");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_StudentFK",
                table: "Projects",
                column: "StudentFK");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectStudents_StudentId",
                table: "ProjectStudents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTeacher_ListofTeachersId",
                table: "ProjectTeacher",
                column: "ListofTeachersId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchProjects_ProjectFK",
                table: "ResearchProjects",
                column: "ProjectFK");

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_CourseFK",
                table: "Specializations",
                column: "CourseFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Projects_ProjectFK",
                table: "Files",
                column: "ProjectFK",
                principalTable: "Projects",
                principalColumn: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_KeywordEnglish_Projects_ProjectFK",
                table: "KeywordEnglish",
                column: "ProjectFK",
                principalTable: "Projects",
                principalColumn: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_KeywordNative_Projects_ProjectFK",
                table: "KeywordNative",
                column: "ProjectFK",
                principalTable: "Projects",
                principalColumn: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Projects_ProjectFK",
                table: "People",
                column: "ProjectFK",
                principalTable: "Projects",
                principalColumn: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Areas_AreaFK",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Collections_CollectionFK",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Collections_CollectionFK",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_OrganicUnits_OrganicUnitFK",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Projects_ProjectFK",
                table: "People");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "KeywordEnglish");

            migrationBuilder.DropTable(
                name: "KeywordNative");

            migrationBuilder.DropTable(
                name: "ProjectStudents");

            migrationBuilder.DropTable(
                name: "ProjectTeacher");

            migrationBuilder.DropTable(
                name: "ResearchProjects");

            migrationBuilder.DropTable(
                name: "Specializations");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "OrganicUnits");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "AccessTypes");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Licenses");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "SchoolYears");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Nationalities");
        }
    }
}
