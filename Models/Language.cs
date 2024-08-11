using System.ComponentModel.DataAnnotations;

namespace Works_Life_Cycle.Models {
    //NOTA: FK de project nao esquecer !!!!!!!!!!!!!!!!!!!!!

    public class Language {
        public Language() {
            ListProjects = new HashSet<Project>();
            ListKeywordNative = new HashSet<KeywordNative>();
        }
        /// <summary>
        /// Language's ID
        /// </summary>
        [Key]
        public int LanguageID { get; set; }

        /// <summary>
        /// Language's Name
        /// </summary>
        [Required]
        [Display(Name = "Linguagem")]
        public string Name { get; set; }
        //Navigation Properties

        public ICollection<Project> ListProjects { get; set; }

        public ICollection<KeywordNative> ListKeywordNative { get; set; }

    }
}