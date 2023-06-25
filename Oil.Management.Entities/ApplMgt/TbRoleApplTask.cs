using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Entities.ApplMgt
{
    [Table("tb_role_appl_task")]
    public class TbRoleApplTask
    {
        [Key, Required]
        [Column("id_appl_task_menu")]
        public int IdApplTaskMenu { get; set; }
        [Key, Required]
        [Column("id_role")]
        public int IdRole { get; set; }
    }
}
