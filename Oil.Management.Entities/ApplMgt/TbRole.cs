using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Entities.ApplMgt
{
    [Table("tb_role")]
    public class TbRole
    {
        [Key, Required]
        [Column("id_role")]
        public int IdRole { get; set; }

        [Column("role_name")]
        public string RoleName { get; set; }
    }
}
