using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Entities.Master
{
    [Table("t_m_inventory_factory")]
    public class TmInventoryFactory
    {
        [Key, Required]
        [Column("id_inventory_factory")]
        public int IdInventoryFactory { get; set; }
        [Column("purchase_order_id_po")]
        public int PurchaseOrderIdPo{ get; set; }
        [Column("date_recieve")]
        public  DateTime DateRecive { get; set; }
        [Column("stock")]
        public double Stock { get; set; }
        [Column("status")]
        public string Status { get; set; }
    }
}
