using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Entities.Master
{
    [Table("t_m_inventory_order")]
    public class TmInventoryOrder
    {
        [Key, Required]
        [Column("id_inventory")]
        public int IdInventory { get; set; }
        [Column("date_recieve")]
        public DateTime DateRecieve{ get; set; }
        [Column("stock")]
        public double Stock { get; set; }
        [Column("created_by")]
        public  int CreatedBy { get; set; } 
        [Column("create_dt")]
        public DateTime CreateDt { get; set; }
        [Column("update_by")]
        public int? UpdateBy { get; set; }  
        [Column("update_dt")]
        public DateTime? UpdateDt { get; set; } 
        [Column("id_po")]
        public  int IdPo { get; set; }
    }
}
