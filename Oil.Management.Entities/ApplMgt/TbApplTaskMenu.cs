using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Entities.ApplMgt
{
    [Table("tb_appl_task_menu")]
    public class TbApplTaskMenu
    {
        [Key, Required]
        [Column("id_appl_task_menu")]
        public int IdApplTaskMenu { get; set; }
        [Column("id_appl_task_parent")]
        public int IdApplTaskParent { get; set; }

        [Column("appl_task_name")]
        public string ApplTaskName { get; set; }

        [Column("controller_name")]
        public string ControllerName { get; set; }
        [Column("action_name")]
        public string ActionName { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("icon_name")]
        public  string IconName { get; set; }
    }
}
