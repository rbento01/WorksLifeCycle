using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Works_Life_Cycle.Models {
    public class Specialization {
        /// <summary>
        /// Specialization's ID
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Specialization's Name
        /// </summary>
        [Required]
        [Display(Name = "Especialização")]
        public string Name { get; set; }

        //1 para 1
        // Se for para meter uma FK, o equivalente ja estara posto na classe Specialization.cs
        // se não estiver a passar os dados será necessário usar fluent api
        // https://www.entityframeworktutorial.net/efcore/configure-one-to-one-relationship-using-fluent-api-in-ef-core.aspx
        ////Navigation Properties
        [ForeignKey(nameof(Course))]
        public int? CourseFK { get; set; }
        public Course Course { get; set; }


    }
}
