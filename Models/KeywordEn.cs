using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Works_Life_Cycle.Models {
    public class KeywordEn {
        [Key]
        public int KeywordEnId { get; set; }

        public string KeywordName { get; set; }



        //Navigation Property 1-N, 1 Project has multiple keywords
        [ForeignKey(nameof(Project))]
        public int? ProjectFK { get; set; }
        public Project Project { get; set; }
    }
}
