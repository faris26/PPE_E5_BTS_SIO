namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("medecin")]
    public partial class medecin
    {
        [Key]
        public int IdMedecin { get; set; }

        [Required]
        [StringLength(50)]
        public string NomMedecin { get; set; }

        [Required]
        [StringLength(50)]
        public string PrenomMedecin { get; set; }

        [Required]
        [StringLength(250)]
        public string AdresseMedecin { get; set; }

        [Required]
        [StringLength(50)]
        public string TelephoneMedecin { get; set; }

        [Required]
        [StringLength(100)]
        public string SpecialiteMedecin { get; set; }

        
        public int? IdSpecialite { get; set; }
        
        public int NumeroDepartement { get; set; }
        
        ////recherche par nom
        //[NotMapped]
        //public string nom { get; set; }
        public virtual departement Departement { get; set; }

        public virtual specialite Specialite { get; set; }
    }
}
