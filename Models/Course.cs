using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Works_Life_Cycle.Models {
    public class Course {
        /// <summary>
        /// id of the course
        /// </summary>
        [Key]
        public int CourseID { get; set; }

        /// <summary>
        /// Code of the course
        /// Ex: MEI-IoT é o M909
        /// </summary>
        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }

        /// <summary>
        /// Name of the course
        /// </summary>
        [Required]
        [Display(Name = "Curso")]
        public string Name { get; set; }

        /// <summary>
        /// Specialization of the course
        /// Ex: "Gestão do Património" 
        /// </summary>
        /// da me ideia de ser 1-1 entao poderia ser um public enum, vou colocar as duas opçoes

        //1 para 1
        // Se for para meter uma FK, o equivalente ja estara posto na classe Specialization.cs
        // se não estiver a passar os dados será necessário usar fluent api
        // https://www.entityframeworktutorial.net/efcore/configure-one-to-one-relationship-using-fluent-api-in-ef-core.aspx
        //Navigation Property
        [ForeignKey(nameof(Specialization))]
        public int? SpecializationFK { get; set; }

        /// <summary>
        /// Area of the course
        /// Ex: área disciplinar do curso segundo a CNAEF
        /// </summary>
        //Navigation Property
        //1 to many relation, 1 area pode ter mais q um curso
        //mas um curso pode nao estar associado a uma area dai tem q ser nullable
        [ForeignKey(nameof(Area))]
        public int? AreaFK { get; set; }
        public Area Area { get; set; }

        /// <summary>
        /// If there is a Partnership with the course, (true or false)
        /// </summary>
        [Required]
        public bool Partnership { get; set; }

        /// <summary>
        /// Repository collection of the course
        /// Para onde os trabalhos do cursos são depositados
        /// Ex: IPT - ESTT - Dissertações de Mestrado ou Doutoramento
        /// </summary>

        //Navigation Property
        //1 para M 1 coleção tem varios cursos
        [ForeignKey(nameof(Collection))]
        public int? CollectionFK { get; set; }
        public Collection Collection { get; set; }
    }
}
