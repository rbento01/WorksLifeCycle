using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Works_Life_Cycle.Models {
    public class Person {


        /// <summary>
        /// ID of the person
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Connection between this model and the one generated with AspNetUser
        /// </summary>
        public string UserNameID { get; set; }

        /// <summary>
        /// Email of the person
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Email of the person
        /// </summary>
        [Display(Name = "Email alternativo")]
        public string? Email2 { get; set; }

        /// <summary>
        /// Name of the person
        /// </summary>
        [Display(Name = "Nome")]
        public string? Name { get; set; }

        /// <summary>
        /// Sex of the person
        /// </summary>
        [Display(Name = "Género")]
        public Gender? Sex { get; set; }

        /// <summary>

        /// Date of birth of the person
        /// </summary>
        [Display(Name = "Data de Nascimento")]
        public string? BirthDate { get; set; }

        /// <summary>
        /// id number of the person
        /// </summary>
        [Display(Name = "Número de identificação")]
        public string? IDNumber { get; set; }

        
        [Display(Name = "Tipo de Identificação")]
        public IDType? IDType { get; set; }

        //1 to many, 1 person can have 1 nationality, but multiple people may be the same nationality
        /// <summary>
        /// Nationality of the person
        /// </summary>
        [Display(Name = "Nacionalidade")]
        [ForeignKey(nameof(Nationality))]
        public int? NationalityFK { get; set; }
        public Nationality? Nationality { get; set; }


        /// <summary>
        /// Address of the person
        /// </summary>
        [Display(Name = "Morada")]
        public string? Address { get; set; }


        /// <summary>
        /// Phone Contact
        /// </summary>
        [Display(Name = "Telemóvel")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "O {0} tem de ter 9 carateres.")]
        public string? Telephone { get; set; }

        /// <summary>
        /// Role of the specified person
        /// </summary>
        public string? Role { get; set; }
    }

    public enum IDType {
        [Display(Name = "Bilhete de identidade nacional ou cartão de cidadão")]
        BIouCC,
        [Display(Name = "Passaporte")]
        Passaport,
        [Display(Name = "Autorização de residência")]
        AuthResidence,
        [Display(Name = "Documento de Identificação estrangeiro")]
        BIForeign,
        [Display(Name = "Certificado de registo de cidadão da União Europeia")]
        CertifRegisterCitizenshipEU,
        [Display(Name = "Cartão de residência permanente de cidadão da União Europeia")]
        PermResidenceCardEU,
        [Display(Name = "Outro")]
        Other,
    }


    public enum Gender {
        Masculino,
        Feminino,
        Outro
    }



}
