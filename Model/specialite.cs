namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("specialite")]
    public partial class specialite
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public specialite()
        {
            Medecins = new HashSet<medecin>();
        }

        [Key]
        public int IdSpecialite { get; set; }

        [Required]
        [StringLength(50)]
        public string LbSpecialite { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<medecin> Medecins { get; set; }
    }
}
