using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Entities.Transaction
{
    [Table("trx_purchase_order_status")]
    public class TrxPurchaseOrderStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_purchase_order_status_detail")]
        public int IdPurchaseOrderStatusDetail { get; set; }
        [Column("id_purchase_order")]
        public int IdPurchaseOrder { get; set; }
        [Column("Status")]
        public int Status { get; set; }
        [Column("description_status")]
        public string DescriptionStatus { get; set; }

        [Column("created_by")]
        public int CreatedBy { get; set; }
        [Column("created_dt")]
        public DateTime CreatedDt { get; set; }

    }
}
