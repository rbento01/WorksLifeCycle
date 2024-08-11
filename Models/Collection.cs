using System.ComponentModel.DataAnnotations;

namespace Works_Life_Cycle.Models {
    public class Collection {
        public Collection() {
            ListProjects = new HashSet<Project>();
            ListCourses = new HashSet<Course>();

        }

        /// <summary>
        /// Collection's ID
        /// </summary>
        [Key]
        public int CollectionID { get; set; }

        /// <summary>
        /// Collection's Name
        /// </summary>
        [Required]
        [Display(Name = "Coleção")]
        public string Name { get; set; }

        //Navigation Properties
        public ICollection<Project> ListProjects { get; set; }

        //Navigation Properties
        public ICollection<Course> ListCourses { get; set; }

        public static implicit operator Collection(List<Collection> v) {
            throw new NotImplementedException();
        }
    }
}
