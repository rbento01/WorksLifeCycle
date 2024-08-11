using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Works_Life_Cycle.Models {
    /// <summary>
    /// Projetos financiados associados a Project
    /// </summary>
    public class ResearchProject {

        /// <summary>
        /// Research Project ID
        /// </summary>
        [Key]
        public int ResearchProjectsID { get; set; }

        /// <summary>
        /// Language's Name
        /// </summary>
        [Required]
        [Display(Name = "Nome")]
        public string Name { get; set; }


        //Navigation Properties

        [ForeignKey(nameof(Project))]
        public int ProjectFK { get; set; }
        public Project Project { get; set; }

    }
}
