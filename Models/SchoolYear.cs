using System.ComponentModel.DataAnnotations;

namespace Works_Life_Cycle.Models {
    public class SchoolYear {
        public SchoolYear() {
            ListProjects = new HashSet<Project>();
        }
        /// <summary>
        /// Id da SchoolYear
        /// </summary>
        [Key]
        public int SchoolYearId { get; set; }

        /// <summary>
        /// SchoolYear's "Name"
        /// </summary>
        [Required]
        [Display(Name = "Ano Letivo")]
        public string Name { get; set; }


        //Relacionamento 1-N, numa epoca ha varios projetos
        //Navigation Property
        public ICollection<Project> ListProjects { get; set; }



    }
}
