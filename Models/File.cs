using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Works_Life_Cycle.Models {
    public class File {

        [Key]
        public int FileId { get; set; }

        public string Path { get; set; }

        public string Name { get; set; }

        //Navigation Properties
        [ForeignKey(nameof(Project))]
        public int? ProjectFK { get; set; }
        public Project Project { get; set; }
    }
}