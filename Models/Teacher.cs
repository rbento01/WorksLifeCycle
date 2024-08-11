using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Works_Life_Cycle.Models {
    public class Teacher : Person {

        public Teacher() {
            ListofProjects = new HashSet<Project>();
        }
        public Boolean External { get; set; }

        public Boolean PhD { get; set; }

        public Boolean Specialist { get; set; }

        /// <summary>
        /// ORCID of the person
        /// </summary>
        [Display(Name = "ORCID(Número de investigador)")]
        public string? ORCID { get; set; }

        //Navigation Property M-M, 1 projeto tem multiplos professores e 1 professor tem múltiplos projetos
        /// <summary>
        /// List of projects the teacher has
        /// </summary>
        public ICollection<Project> ListofProjects { get; set; }

    }
}
