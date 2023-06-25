using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Entities.Master
{
    [Table("t_m_sales_type")]
    public class TmSalesType
    {
        [Key,Required]
        [Column("id_sales_type")]
        public int IdSalesType { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("status")]
        public int Status { get; set; } 
    }
}
