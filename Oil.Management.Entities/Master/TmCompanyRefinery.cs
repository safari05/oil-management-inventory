using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Entities.Master
{
    [Table("t_m_company_refinery")]
    public class TmCompanyRefinery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_company_refinery")]
        public int IdCompanyRefinery { get; set; }

        [Column("id_purchase")]
        public int IdPurchase { get; set; }
        [Column("date_recieve")]
        public DateTime DateRecieve { get; set; }

        [Column("stock")]
        public double  Stock { get;set; }
        [Column("id_company")]
        public int IdCompany { get; set; }
    }
}
