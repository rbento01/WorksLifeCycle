using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Works_Life_Cycle.Models {
    public class Project {
        public Project() {
            // inicializar a lista de Categorias do Livro
            Files = new HashSet<File>();
            ResearchProjects = new HashSet<ResearchProject>();
            ListKeywordNative = new HashSet<KeywordNative>();
            ListKeywordEnglish = new HashSet<KeywordEn>();
            ListofTeachers = new HashSet<Teacher>();
        }
        /// <summary>
        /// ID of the project
        /// </summary>
        /// se quisermos seguir os code conventions nao é necessario a tag [key], a Framework consegue determinar o ID como PK sozinha ao "inferir"
        [Key]
        public int ProjectId { get; set; } //

        /// <summary>
        /// Title of the project
        /// </summary>

        [Display(Name = "Título")]
        public string? Title { get; set; } //

        /// <summary>
        /// ID interno do projeto associado pelo IPT
        /// </summary>
        
        [Display(Name = "ID interno")]
        public int? InternalID { get; set; } //

        /// <summary>
        /// Summary of the project in PT
        /// </summary>
        /// 
        [Display(Name = "Resumo")]
        public string? AbstractPT { get; set; } //
        /// <summary>
        /// Summary of the project in EN
        /// </summary>
        [Display(Name = "Resumo(EN)")]
        public string? AbstractEN { get; set; } //

        /// <summary>
        /// Comments of the project
        /// </summary>
        /// 
        [Display(Name = "Comentários")]
        public string? Comments { get; set; } //

        [Display(Name = "Estado")]
        public Status? Status { get; set; } 

        /// <summary>
        /// Thesis ID - o ID do trabalho de mestrado ou doutoramento que vai ser gerado pelo RENATES
        /// </summary>
        /// 
        [Display(Name = "Thesis ID")]
        public int? TID { get; set; } //
        /// <summary>
        /// URL no repositório institucional do RCAAP, caso já tenha sido depositado
        /// </summary>
        /// 
        [Display(Name = "URL no RCAAP")]
        public string? Handle { get; set; }
        [Display(Name = "Nota Final")]
        public int? Grade { get; set; }
        [Display(Name = "Data da Defesa")]
        public DateTime? DefenceDate { get; set; } //

        //Navigation Property
        //1-N, 1 Project has multiple keywords
        public ICollection<KeywordNative>? ListKeywordNative { get; set; }
        //Navigation Property
        //1-N, 1 Project has multiple keywords
        public ICollection<KeywordEn>? ListKeywordEnglish { get; set; }
        //Navigation Property
        //1 to many, 1 projeto pode ter varios ficheiros
        public ICollection<File>? Files { get; set; }
        //Navigation Property
        //1 projeto pode ter varios Research Projects
        public ICollection<ResearchProject>? ResearchProjects { get; set; }
        //Navigation Property
        //Relacionamento M-M, 1 projeto tem varios professores/coordenadores, 1 professor tem varios projetos
        public virtual ICollection<Teacher>? ListofTeachers { get; set; }



        //Relacionamento 1-1, 1 projeto tem um aluno e vice-versa
        //Navigation Property
        [ForeignKey(nameof(Student))] //
        public int? StudentFK { get; set; }
        public Student? Student { get; set; }

        //Relacionamento 1-N, 1 projeto tem um acess type, mas 1 acesstype ta associado a varios Projetos
        //Navigation Property
        
        [ForeignKey(nameof(AccessType))] //
        public int? AccessTypeFK { get; set; }
        public AccessType? AccessType { get; set; }

        //Relacionamento 1-N, 1 projeto tem uma so epoca, mas varios projetos estao na msm epoca
        //Navigation Property
        [ForeignKey(nameof(SchoolYear))] //
        public int? SchoolYearFK { get; set; }
        public SchoolYear? SchoolYear { get; set; }



        //Afinal 1 projeto so tem 1 linguagem, 1 linguagem é q é utilizada em varios projetos

        [ForeignKey(nameof(Language))] //
        public int? LanguageFK { get; set; }
        public Language? Language { get; set; }



        //Relacionamento 1-N, 1 coleção tem varios projetos
        //Navigation Property
        [ValidateNever]
        [ForeignKey(nameof(Collection))] //
        public int? CollectionFK { get; set; }
        public Collection? Collection { get; set; }

        //1 project has 1 license, while 1 license has multiple projects associated with it
        [ForeignKey(nameof(License))] //
        public int? LicenseFK { get; set; }
        public License? License { get; set; }

        //Relacionamento 1-N, 1 projeto tem um so course, mas varios projectos estao no msm course
        //Navigation Property
        [ForeignKey(nameof(Course))] //
        public int? CourseFK { get; set; }
        public Course? Course { get; set; }

    }
    public enum Status {
        [Display(Name = "N/A")]
        NA,
        [Display(Name = "Submetido para publicação (Submitted Version)")]
        Submetido,
        [Display(Name = "Aceite para publicação (Accepted Version)")]
        Aceite,
        [Display(Name = "Publicado (Published Version)")]
        Publicado,
        [Display(Name = "Não publicado (Draft)")]
        NaoPublicado,
        [Display(Name = "Atualização (Updated Version)")]
        Atualizacao
    }

}