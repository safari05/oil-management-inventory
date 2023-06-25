using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Entities.Master
{
    [Table("t_m_subsidiary_company")]
    public class TmSubsidiaryCompany
    {
        [Key, Required]
        [Column("id_subsidiary_company")]
        public int IdSubsidiaryCompany { get; set; }
        [Column("id_factory")]
        public int IdFactory { get; set; }
        [Column("id_bussiness_unit")]
        public int IdBussinessUnit { get; set; }
        [Column("name")]
        public string Name { get; set; }    
        [Column("nib")]
        public string Nib { get; set; }
        [Column("npwp")]
        public string Npwp { get; set; }    
        [Column("phone")]
        public string Phone { get;set; }

        [Column("fax")]
        public string Fax { get; set; } 
        [Column("email")]
        public  string Email { get; set; }
        [Column("status")]
        public int Status { get; set; } 
        [Column("description")]
        public string Description { get; set; }
        public string PicName { get; set; }
        [Column("pic_phone")]
        public string PicPhone { get; set; }
        [Column("pic_email")]
        public string PicEmail { get; set; }
        [Column("priority")]
    }
}
