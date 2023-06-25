using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Entities.References
{
    [Table("t_ref_template_broadcast_message")]
    public class TrRefTemplateBroadcastMessage
    {
        [Key,Required]
        [Column("id_ref_brodcast_message")]
        public int IdRefTemplateBroadcastMessage{ get; set; }
        [Column("brodcast_message_name")]
        public string BroadcastMessageName { get; set; }
        [Column("message")]
        public string Message { get; set; }
    }
}
