using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Entities.ApplMgt
{
    [Table("tb_user_role")]
    public class TbUserRole
    {
        [Key, Required]
        [Column("id_user")]
        public int IdUser { get; set; }
        [Key, Required]
        [Column("id_role")]
        public int IdRole { get; set; }
    }
}
