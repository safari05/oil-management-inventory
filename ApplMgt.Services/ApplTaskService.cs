using Dapper;
using Oil.Management.Entities.ApplMgt;
using Oil.Management.Shared;
using Oil.Management.Shared.Interfaces;
using Oil.Management.Shared.ViewModels.ApplMgt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplMgt.Services
{
    public class ApplTaskService : IApplTaskService
    {
        private readonly string ServiceName = "ApplicationManagement.Services.ApplTaskService.";
        private readonly Common common = new Common();
        public List<ApplTaskModel> GetMenus(int IdAppl, int IdUser, out string oMessage)
        {
            oMessage = string.Empty;
            try
            {
                using (IDbConnection conn = common.DbConnection)
                {
                    conn.Open();
                    var ret = (from a in conn.GetList<TbUserRole>()
                               join b in conn.GetList<TbRole>() on a.IdRole equals b.IdRole
                               join c in conn.GetList<TbRoleApplTask>() on a.IdRole equals c.IdRole
                               join d in conn.GetList<TbApplTaskMenu>() on c.IdApplTaskMenu equals d.IdApplTaskMenu
                               where a.IdUser == IdUser
                               select new ApplTaskModel
                               {
                                   IdApplTask = d.IdApplTaskMenu,
                                   ActionName = d.ActionName,
                                   ApplTaskName = d.ApplTaskName,
                                   ControllerName = d.ControllerName,
                                   Description = d.Description,
                                   IdApplTaskParent = d.IdApplTaskParent,
                                   IconName = d.IconName
                               }).OrderBy(x => x.IdApplTask).ToList();
                    return ret;
                }
            }
            catch (Exception ex)
            {
                oMessage = common.GetErrorMessage(ServiceName + "GetMenus", ex);
                return null;
            }
        }
    }
}
