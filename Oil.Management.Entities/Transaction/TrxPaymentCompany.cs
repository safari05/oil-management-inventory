using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Entities.Transaction
{
    [Table("trx_payment_company")]
    public class TrxPaymentCompany
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_payment")]
        public int IdPayment{ get; set; }

        [Column("id_po")]
        public int IdPo { get; set; }

        [Column("amount_bill")]
        public double AmountBill { get;set; }

        [Column("amount_difference")]
        public double AmountDifference { get;set; }

        [Column("status")]
        public int Status { get; set; }
    }
}
