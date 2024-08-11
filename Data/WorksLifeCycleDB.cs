using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Works_Life_Cycle.Models;

namespace Works_Life_Cycle.Data {
    public class WorksLifeCycleDB : IdentityDbContext {

        public WorksLifeCycleDB(DbContextOptions<WorksLifeCycleDB> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            ////isto é necessario para relacionamentos 1 para 1, pois a EF nao consegue interpretar solo, entao temos q definir explicitamente
            ////https://docs.microsoft.com/pt-pt/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#one-to-one
            //modelBuilder.Entity<Course>()
            //.HasOne(b => b.Specializations)
            //.WithOne(c => c.Course)
            //.HasForeignKey<Specialization>(b => b.CourseForeignKey);



            // https://entityframework.net/many-to-many-relationship
            //link para relacionamentos M-M
            //Na EF 6 nao basta apenas usar data annotations temos q definir assim a chave composta
            modelBuilder.Entity<ProjectStudent>().HasKey(p => new { p.ProjectId, p.StudentId });



            // adicionar os Roles
            modelBuilder.Entity<IdentityRole>().HasData(
               new IdentityRole { Id = "s", Name = "Student", NormalizedName = "STUDENT" },
               new IdentityRole { Id = "t", Name = "Teacher", NormalizedName = "TEACHER" },
               new IdentityRole { Id = "c", Name = "Secretary", NormalizedName = "SECRETARY" },
               new IdentityRole { Id = "a", Name = "Admin", NormalizedName = "ADMIN" }
            );

            modelBuilder.Entity<Course>().HasData(
               new Course { CourseID = 1, Code = "M932", Name = "Analítica e Inteligência Organizacional"/*, AreaFK = 1*/ },
               new Course { CourseID = 2, Code = "6498", Name = "Arqueologia Pré-Histórica e Arte Rupestre" },
               new Course { CourseID = 3, Code = "M925", Name = "Auditoria e Finanças" },
               new Course { CourseID = 4, Code = "MB41", Name = "Avaliação e Gestão de Ativos Imobiliários" },
               new Course { CourseID = 5, Code = "9405", Name = "Conservação e Restauro" },
               new Course { CourseID = 6, Code = "M013", Name = "Desenvolvimento de Produtos de Turismo Cultural" },
               new Course { CourseID = 7, Code = "M474", Name = "Design Editorial" },
               new Course { CourseID = 8, Code = "M778", Name = "Engenharia Eletrotécnica" },
               new Course { CourseID = 9, Code = "M909", Name = "Engenharia Informática-Internet das Coisas" },
               new Course { CourseID = 10, Code = "M746", Name = "Engenharia Mecânica - Projecto e Produção Mecânica" },
               new Course { CourseID = 11, Code = "9299", Name = "Gestão de Recursos Humanos" },
               new Course { CourseID = 12, Code = "9295", Name = "Gestão" },
               new Course { CourseID = 13, Code = "M199", Name = "Reabilitação Urbana" },
               new Course { CourseID = 14, Code = "M079", Name = "Tecnologia Química" },
               new Course { CourseID = 15, Code = "M197", Name = "Técnicas de Arqueologia" }

           );

            modelBuilder.Entity<Area>().HasData(
                new Area { AreaID = 1, Name = "Ciências Informáticas" },
                new Area { AreaID = 2, Name = "História e Arqueologia" },
                new Area { AreaID = 3, Name = "Finanças, Banca e Seguros" },
                new Area { AreaID = 4, Name = "Artesanato" },
                new Area { AreaID = 5, Name = "Turismo e Lazer" },
                new Area { AreaID = 6, Name = "Design" },
                new Area { AreaID = 7, Name = "Electrónica e Automação" },
                new Area { AreaID = 8, Name = "Ciências Informáticas" },
                new Area { AreaID = 9, Name = "Metalurgia e Metalomecânica" },
                new Area { AreaID = 10, Name = "Gestão e Administração" },
                new Area { AreaID = 11, Name = "Arquitectura e Urbanismo" },
                new Area { AreaID = 12, Name = "Tecnologia dos Processos Químicos" }
            );


            modelBuilder.Entity<Collection>().HasData(
                new Collection { CollectionID = 1, Name = "IPT - ESTT - Dissertações de Mestrado ou Doutoramento" },
                new Collection { CollectionID = 2, Name = "IPT - ESGT - Dissertações de Mestrado ou Doutoramento" },
                new Collection { CollectionID = 3, Name = "IPT - ESTA - Dissertações de Mestrado ou Doutoramento" },
                new Collection { CollectionID = 4, Name = "IPT - CGeo - Dissertações de Mestrado" },
                new Collection { CollectionID = 5, Name = "IPT - Ci2 - Dissertações de Mestrado" },
                new Collection { CollectionID = 6, Name = "IPT - Techn&Art - Dissertações de Mestrado" }

            );

            modelBuilder.Entity<Specialization>().HasData(
                new Specialization { Id = 1, Name = "Sem especialização" },
                new Specialization { Id = 2, Name = "Área de especialização: Gestão do Património Cultural" }
            );

            modelBuilder.Entity<Language>().HasData(
                new Language { LanguageID = 1, Name = "Português" },
                new Language { LanguageID = 2, Name = "Inglês" },
                new Language { LanguageID = 3, Name = "Espanhol" },
                new Language { LanguageID = 4, Name = "Alemão" },
                new Language { LanguageID = 5, Name = "Francês" },
                new Language { LanguageID = 6, Name = "Italiano" },
                new Language { LanguageID = 7, Name = "Mirandês" },
                new Language { LanguageID = 8, Name = "Japonês" },
                new Language { LanguageID = 9, Name = "Chinês" },
                new Language { LanguageID = 10, Name = "Outro" },
                new Language { LanguageID = 11, Name = "N/A" }
            );

            modelBuilder.Entity<SchoolYear>().HasData(
                new SchoolYear { SchoolYearId = 1, Name = "2019/2020" },
                new SchoolYear { SchoolYearId = 2, Name = "2020/2021" },
                new SchoolYear { SchoolYearId = 3, Name = "2021/2022" }
                );

            modelBuilder.Entity<AccessType>().HasData(
                new AccessType { AccessTypeId = 1, Name = "Acesso livre" },
                new AccessType { AccessTypeId = 2, Name = "Acesso Embargado(1 ano)" },
                new AccessType { AccessTypeId = 3, Name = "Acesso Embargado(2 ano)" }
                );

            modelBuilder.Entity<Nationality>().HasData(
                new Nationality { NationalityId = 1, Name = "Portuguesa", Acronym = "PT" },
                new Nationality { NationalityId = 2, Name = "Espanhola", Acronym = "ES" },
                new Nationality { NationalityId = 3, Name = "Britânica", Acronym = "UK" },
                new Nationality { NationalityId = 4, Name = "Francesa", Acronym = "FR" },
                new Nationality { NationalityId = 5, Name = "Alemã", Acronym = "DE" },
                new Nationality { NationalityId = 6, Name = "Holandesa", Acronym = "NL" },
                new Nationality { NationalityId = 7, Name = "Suiça", Acronym = "CH" },
                new Nationality { NationalityId = 8, Name = "Checa", Acronym = "CZ" },
                new Nationality { NationalityId = 9, Name = "Polaca", Acronym = "PL" },
                new Nationality { NationalityId = 10, Name = "Dinamarquesa", Acronym = "DK" },
                new Nationality { NationalityId = 11, Name = "Sueca", Acronym = "SE" },
                new Nationality { NationalityId = 12, Name = "Turca", Acronym = "TR" },
                new Nationality { NationalityId = 13, Name = "Italiana", Acronym = "IT" },
                new Nationality { NationalityId = 14, Name = "Americana", Acronym = "US" },
                new Nationality { NationalityId = 15, Name = "Russa", Acronym = "RU" },
                new Nationality { NationalityId = 16, Name = "Outra", Acronym = "XX" }
                );
            modelBuilder.Entity<License>().HasData(
                new License { LicenseId = 1, LicenseName = "Sem Licença" },
                new License { LicenseId = 2, LicenseName = "Atribuição (CC-BY)" },
                new License { LicenseId = 3, LicenseName = "Atribuição, Sem Trabalhos Derivados (CC-BY-ND)" },
                new License { LicenseId = 4, LicenseName = "Atribuição, Partilha nos Termos da Mesma Licença (CC-BY-SA)" },
                new License { LicenseId = 5, LicenseName = "Atribuição, Uso Não Comercial (CC-BY-NC)" },
                new License { LicenseId = 6, LicenseName = "Atribuição, Não Comercial, Sem Derivações (CC-BY-NC-ND)" },
                new License { LicenseId = 7, LicenseName = "Atribuição, Uso Não Comercial, Partilha nos Termos da Mesma Licença(CC - BY - NC - SA)" }
                );

        }

        public DbSet<Works_Life_Cycle.Models.Area> Areas { get; set; }
        public DbSet<Works_Life_Cycle.Models.Collection> Collections { get; set; }
        public DbSet<Works_Life_Cycle.Models.Course> Courses { get; set; }
        public DbSet<Works_Life_Cycle.Models.File> Files { get; set; }
        public DbSet<Works_Life_Cycle.Models.Language> Languages { get; set; }
        public DbSet<Works_Life_Cycle.Models.Person> People { get; set; }
        public DbSet<Works_Life_Cycle.Models.Project> Projects { get; set; }
        public DbSet<Works_Life_Cycle.Models.ResearchProject> ResearchProjects { get; set; }
        public DbSet<Works_Life_Cycle.Models.Specialization> Specializations { get; set; }
        public DbSet<Works_Life_Cycle.Models.Student> Students { get; set; }
        public DbSet<Works_Life_Cycle.Models.Teacher> Teachers { get; set; }
        public DbSet<Works_Life_Cycle.Models.AccessType> AccessTypes { get; set; }
        public DbSet<Works_Life_Cycle.Models.SchoolYear> SchoolYears { get; set; }
        public DbSet<Works_Life_Cycle.Models.KeywordNative> KeywordNative { get; set; }
        public DbSet<Works_Life_Cycle.Models.KeywordEn> KeywordEnglish { get; set; }
        public DbSet<Works_Life_Cycle.Models.Nationality> Nationalities { get; set; }
        public DbSet<Works_Life_Cycle.Models.License> Licenses { get; set; }
        public DbSet<Works_Life_Cycle.Models.ProjectStudent> ProjectStudents { get; set; }


    }
}