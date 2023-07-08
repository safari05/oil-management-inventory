using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Entities.References
{
    [Table("tb_type_user")]
    public class TbTypeUser
    {
        [Key,Required]
        [Column("id_type_user")]
        public int IdTypeUser { get; set; }
        [Column("type_name")]
        public string TypeName { get; set; }
    }
}
