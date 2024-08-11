using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Works_Life_Cycle.Models {
    public class KeywordNative {
        [Key]
        public int KeywordNativeId { get; set; }

        public string KeywordName { get; set; }

        //Navigation Property 1-N, A language may have multiple keywords
        //need to add option for user to select his native language
        [ForeignKey(nameof(Language))]
        public int? LanguageFK { get; set; }
        public Language Language { get; set; }


        //Navigation Property 1-N, 1 Project has multiple keywords
        [ForeignKey(nameof(Project))]
        public int? ProjectFK { get; set; }
        public Project Project { get; set; }
    }
}
