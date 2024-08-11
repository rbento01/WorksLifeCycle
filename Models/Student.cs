using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Works_Life_Cycle.Models {
    public class Student : Person {


        /// <summary>
        /// ProjectId of the student
        /// </summary>
        [Required]
        [Display(Name = "Nº de aluno")]
        public int StudentId { get; set; }
        /// <summary>
        /// Concat of the studentID and Name
        /// </summary>
        public string Combo {
            get {
                return string.Format("{0} - {1}", StudentId, Name);
            }

        }

        //Relacionamento 1-1, 1 aluno tem um so projeto, 
        //Navigation Property
        [ForeignKey(nameof(Project))] //
        public int? ProjectFK { get; set; }
        public Project? Project { get; set; }

        //Relacionamento 1-N, 1 aluno tem um so course, mas varios alunos estao no msm course
        //Navigation Property
        [ForeignKey(nameof(Course))] //
        public int? CourseFK { get; set; }
        public Course? Course { get; set; }


    }
}
