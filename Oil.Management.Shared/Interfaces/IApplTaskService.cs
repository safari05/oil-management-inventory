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
    }
}
