using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Entities.Transaction
{

    [Table("trx_purchase_order")]
    public class TrxPurchaseOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_po")]
        public int IdPo { get; set; }
        [Column("code_po")]
        public string CodePo { get; set; }

        [Column("name_po")]
        public string NamePo { get; set; }

        [Column("total_order")]
        public double TotalOrder{ get; set; }

        [Column("satuan")]
        public string Satuan { get; set; }

        [Column("amount")]
        public double Amount { get; set; }

        [Column("time_arrival")]
        public DateTime? TimeArrival { get;set; } 

        [Column("status")]
        public int Status { get; set; }

        [Column("tax")]
        public int Tax { get; set; }

        [Column("created_by")]
        public int CreatedBy { get; set; }

        [Column("created_dt")]
        public DateTime CreatedDt { get; set; }

        [Column("updated_by")]
        public int? UpdatedBy { get; set; }

        [Column("updated_dt")]
        public DateTime? UpdatedDt { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("id_contract")]
        public int IdContract { get;set; }





    }
}
