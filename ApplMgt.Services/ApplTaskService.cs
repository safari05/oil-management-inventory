using Dapper;
using Oil.Management.Entities.ApplMgt;
using Oil.Management.Shared;
using Oil.Management.Shared.Interfaces;
using Oil.Management.Shared.ViewModels;
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

        public List<ApplTaskModel> GetApplTasks(int IdAppl, out string oMessage)
        {
            try
            {
                oMessage = string.Empty;
                List<ApplTaskModel> ret = new List<ApplTaskModel>();
                using (var conn = common.DbConnection)
                {
                    var tbApplTasks = (from a in conn.GetList<TbApplTaskMenu>()
                                       select new { a}).ToList();
                    if (tbApplTasks == null || tbApplTasks.Count() == 0) { oMessage = "data tidak ada"; return null; }
                    foreach (var tbApplTask in tbApplTasks.OrderBy(x => x.a.IdApplTaskMenu))
                    {
                        ApplTaskModel m = new ApplTaskModel
                        {
                            ApplName = "",
                            ActionName = tbApplTask.a.ActionName,
                            ApplTaskName = tbApplTask.a.ApplTaskName,
                            ControllerName = tbApplTask.a.ControllerName,
                            Description = tbApplTask.a.Description,
                            IconName = tbApplTask.a.IconName,
                            IdAppl = 0,
                            IdApplTask = tbApplTask.a.IdApplTaskMenu,
                            IdApplTaskParent = tbApplTask.a.IdApplTaskParent
                        };
                        ret.Add(m);
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                oMessage = common.GetErrorMessage(ServiceName + "GetApplTasks", ex);
                return null;
            }
        }

        public ApplTaskListModel GetApplTasks(int IdAppl, ReqDataTableModel Data, out string oMessage)
        {
            try
            {
                oMessage = string.Empty;
                ApplTaskListModel ret = new ApplTaskListModel();
                using (var conn = common.DbConnection)
                {
                    var tbApplTasks = (from a in conn.GetList<TbApplTaskMenu>()
                                       select new { a }).ToList();
                    if (tbApplTasks == null || tbApplTasks.Count() == 0)
                    {
                        oMessage = "data tidak ada";
                        ret.RecordCount = 0;
                        return null;
                    }

                    ret.RecordCount = tbApplTasks.Count();
                    ret.Data = new List<ApplTaskModel>();
                    var dataApplTaskPage = tbApplTasks.Skip(Data.PageStart).Take(Data.RecordPerPage).ToList();

                    foreach (var tbApplTask in dataApplTaskPage)
                    {
                        ApplTaskModel m = new ApplTaskModel
                        {
                            ApplName = "",
                            ActionName = tbApplTask.a.ActionName,
                            ApplTaskName = tbApplTask.a.ApplTaskName,
                            ControllerName = tbApplTask.a.ControllerName,
                            Description = tbApplTask.a.Description,
                            IconName = tbApplTask.a.IconName,
                            IdAppl = 0,
                            IdApplTask = tbApplTask.a.IdApplTaskMenu,
                            IdApplTaskParent = tbApplTask.a.IdApplTaskParent
                        };
                        ret.Data.Add(m);
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                oMessage = common.GetErrorMessage(ServiceName + "GetApplTasks", ex);
                return null;
            }
        }

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
