using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Entities.Master
{
    [Table("t_m_suppliers")]
    public class TmSuppliers
    {
        [Key,Required]
        [Column("id_company")]
        public int IdCompany { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("nib")]
        public string Nib { get; set; }
        [Column("phone")]
        public string Phone { get; set; }   
        [Column("npwp")]
        public string Npwp { get; set; }    
        [Column("email")]
        public string Email { get; set; }   
        [Column("website")]
        public string Website { get; set; } 
    }
}
