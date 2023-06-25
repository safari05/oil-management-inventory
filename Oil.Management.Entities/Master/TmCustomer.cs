using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Entities.Master
{
    [Table("t_m_customer")]
    public class TmCustomer
    {
        [Key,Required]
        [Column("id_customer")]
        public int CustomerID { get; set; }
        [Column("customer_name")]
        public string CustomerName { get; set; }    
        [Column("nib")]
        public string  Nib { get; set; }    
        [Column("npwp")]
        public  string Npwp { get; set; }   
        [Column("phone")]
        public string Phone { get; set; }
        [Column("email")]
        public  string Email { get; set; }  
        [Column("website")]
        public  string  Website { get; set; }   
        [Column("address")]
        public string Address { get; set; } 
        [Column("pic_name")]
        public  string PicName { get; set; }    
        [Column("pic_phone")]
        public string PicPhone { get; set; }
        [Column("pic_email")]
        public  string PicEmail { get; set; }
        [Column("priority")]
        public bool Priority { get; set; }
        [Column("id_subsidiary_company")]
        public int IdSubsidiaryCompany { get; set; }
        [Column("id_village")]
        public int IdVillage { get; set; }

    }
}
