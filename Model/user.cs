using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("user")]
    public class user
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(30)]
        public string motdepasse{ get; set; }

        [Required]
        [StringLength(30)]
        public string identifiant { get; set; }
    }
}