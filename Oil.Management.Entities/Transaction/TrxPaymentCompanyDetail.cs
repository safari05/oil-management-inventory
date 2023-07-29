using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Entities.Transaction
{
    [Table("trx_payment_company_detail")]
    public class TrxPaymentCompanyDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_payment_company_detail")]
        public int IdPaymentCompanyDetail { get; set; }

        [Column("id_payment_company")]
        public int IdPaymentCompany { get; set; }
        [Column("amount")]

        public double Amount { get; set; }  
        [Column("proof_payment")]
        public string ProofPayment { get;set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("created_by")]
        public int CreatedBy { get; set; }

        [Column("created_dt")]
        public DateTime CreatedDt { get; set; }

        [Column("updated_by")]
        public int? UpdatedBy { get; set; }

        [Column("updated_dt")]
        public DateTime? UpdatedDt { get; set; }

        
    }
}
