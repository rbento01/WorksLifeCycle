using System.ComponentModel.DataAnnotations;

namespace Works_Life_Cycle.Models {
    public class License {
        public License() {
            ListofProjects = new HashSet<Project>();
        }
        [Key]
        public int LicenseId { get; set; }
        [Display(Name = "Licença")]
        public string LicenseName { get; set; }

        //1 type of license has multiple projects associated with it, while 1 project only has 1 license
        public ICollection<Project> ListofProjects { get; set; }
    }
}
