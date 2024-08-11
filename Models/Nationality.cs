using System.ComponentModel.DataAnnotations;

namespace Works_Life_Cycle.Models {
    public class Nationality {

        public Nationality() {
            ListofPersons = new HashSet<Person>();
        }
        [Key]
        public int NationalityId { get; set; }
        /// <summary>
        /// Designação da nacionalidade: Portugal -> Português
        /// </summary>
        /// 
        [Display(Name = "Nacionalidade")]
        public string Name { get; set; }
        /// <summary>
        /// Acronimo da Nacionalidade: Portugal -> PT
        /// </summary>
        /// 
        [Display(Name = "Acrónimo")]
        public string Acronym { get; set; }

        /// <summary>
        /// Lista de pessoas com a mesma nacionalidade
        /// </summary>
        public ICollection<Person> ListofPersons { get; set; }


    }
}
