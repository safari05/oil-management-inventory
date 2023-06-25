using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Shared.ViewModels.ApplMgt
{
    public class ApplTaskListModel
    {
        public int RecordCount { get; set; }
        public List<ApplTaskModel> Data { get; set; }
    }
    public class ApplTaskModel : ApplTaskAddModel
    {
        public int IdApplTask { get; set; }
        public string ApplName { get; set; }
    }

    public class ApplTaskMenuModel
    {

        public int? IdApplTaskParent { get; set; }
        public string ApplTaskName { get; set; }
        public int IdApplTask { get; set; }
        public string ApplName { get; set; }
        public int Level { get; set; }
    }

    public class ApplTaskAddModel
    {
        public int? IdApplTaskParent { get; set; }
        public string ApplTaskName { get; set; }
        public int IdAppl { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Description { get; set; }
        public string IconName { get; set; }
    }
}
