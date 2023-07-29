using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Entities.Master
{
    [Table("tb_m_factory_oil")]
    public class TbMFactoryOil
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_factory")]
        public int IdFactory { get; set; }
        [Column("factory_name")]
        public string FactoryName { get; set; }
        [Column("nib")]
        public string Nib { get; set; }

        [Column("pic")]
        public string Pic{ get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("email")]
        public string Email{ get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("status")]
        public int Status{ get; set; }

        [Column("created_by")]
        public int CreatedBy { get; set; }
        
        [Column("created_dt")]
        public DateTime CreatedDt { get; set; }

        [Column("updated_by")]
        public int? UpdatedBy { get; set; }

        [Column("updated")]
        public DateTime? UpdatedDate { get; set; }

    }
}
