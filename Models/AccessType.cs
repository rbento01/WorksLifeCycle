using System.ComponentModel.DataAnnotations;

namespace Works_Life_Cycle.Models {
    public class AccessType {
        public AccessType() {
            ListProjects = new HashSet<Project>();
        }
        /// <summary>
        /// Acess Type ID
        /// </summary>
        [Key]
        public int AccessTypeId { get; set; }

        /// <summary>
        /// Acess Type Name(Designation)
        /// </summary>
        [Required]
        [Display(Name = "Tipo de Acesso")]
        public string Name { get; set; }


        ////Navigation Properties
        public ICollection<Project> ListProjects { get; set; }

        public static implicit operator AccessType(string v) {
            throw new NotImplementedException();
        }
    }
}
