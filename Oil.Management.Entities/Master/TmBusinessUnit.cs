using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Entities.Master
{   
    [Table("t_m_bussiness_unit")]
    public class TmBusinessUnit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_bussiness_unit")]
        public int IdBussinessUnit { get; set; }

        [Column("business_name")]
        public string BussinessName { get; set; }

        [Column("status")]
        public int Status { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }

        [Column("updated")]
        public DateTime? Updated { get; set; }
    }
}
