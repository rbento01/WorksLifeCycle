using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// https://entityframework.net/many-to-many-relationship
//relação M-M usando data annotations

namespace Works_Life_Cycle.Models {
    public class ProjectStudent {
        [Key, Column(Order = 1)]
        public int StudentId { get; set; }
        [Key, Column(Order = 2)]
        public int ProjectId { get; set; }

        //name of the student
        public string Name { get; set; }

        public Student Student { get; set; }
        public Project Project { get; set; }
        /// <summary>
        /// AcceptanceStatus do grupo para o projeto, se foi aceite ou não
        /// </summary>
        public Boolean AcceptanceStatus { get; set; }
    }
}
