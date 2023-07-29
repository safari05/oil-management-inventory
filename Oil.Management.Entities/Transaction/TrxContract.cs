using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Entities.Transaction
{
    [Table("trx_contract")]
    public class TrxContract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_contract")]
        public int IdContract { get; set; }
        [Column("name_contract")]
        public string NameContract { get; set; }
        [Column("start_contract")]
        public DateTime StartContract { get; set; }

        [Column("end_contract")]
        public DateTime EndContract { get; set; }

        [Column("file_guarante_archive")]
        public string FileGuaranteArchive{ get; set; }

        [Column("pct_domostic")]
        public int PctDomestic{ get; set; }
        [Column("pct_ekspor")]
        public int PctEkspor{ get; set; }
        [Column("status")]
        public int Status{ get; set; }
        [Column("description")]
        public string Description{ get; set; }
        [Column("created_by")]
        public int CreatedBy{ get; set; }
        [Column("created_dt")]
        public DateTime CreatedDt{ get; set; }
        [Column("id_factory")]
        public int IdFactory{ get; set; }

        [Column("updated_by")]
        public int? UpdatedBy { get; set; }
        [Column("updated_dt")]
        public DateTime? UpdatedDt { get; set; }
    }
}
