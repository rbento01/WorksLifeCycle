using System.ComponentModel.DataAnnotations;

namespace Works_Life_Cycle.Models {
    public class Area {
        public Area() {
            ListCourse = new HashSet<Course>();
        }
        /// <summary>
        /// Area's ID
        /// </summary>
        [Key]
        public int AreaID { get; set; }

        /// <summary>
        /// Area's name
        /// </summary>
        [Required]
        [Display(Name = "Area")]
        public string Name { get; set; }

        //Navigation Properties
        public ICollection<Course> ListCourse { get; set; }
    }
}
