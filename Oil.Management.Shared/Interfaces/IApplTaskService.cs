using Oil.Management.Shared.ViewModels;
using Oil.Management.Shared.ViewModels.ApplMgt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Shared.Interfaces
{
    public interface IApplTaskService
    {
        // auth menu
        List<ApplTaskModel> GetMenus(int IdAppl, int IdUser, out string oMessage);
        List<ApplTaskModel> GetApplTasks(int IdAppl, out string oMessage);
        ApplTaskListModel GetApplTasks(int IdAppl, ReqDataTableModel Data, out string oMessage);
    }
}
