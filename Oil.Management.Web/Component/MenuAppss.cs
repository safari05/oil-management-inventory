using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oil.Management.Shared.Constants;
using Oil.Management.Shared.Interfaces;
using Oil.Management.Shared.ViewModels.ApplMgt;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;



namespace Oil.Management.Web.Component
{
    public class MenuAppss : ViewComponent
    {
        public const string OPEN_LIST_TAG = "<ul>";
        public const string CLOSE_LIST_TAG = "</ul>";
        public const string OPEN_LIST_ITEM_TAG = "<li>";
        public const string CLOSE_LIST_ITEM_TAG = "</li>";
        public const string OPEN_LIST_ITEM_TAG_CLASS = "<li class='nav-item dropdown'>";
        public const string OPEN_LIST_UL_CLASS_DROPDOWN = "<ul class='dropdown-menu'>";

        private readonly IApplTaskService _applTaskService;
        private readonly int IdUser;
        IList<ApplTaskModel> allMenuItems;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public MenuAppss(IHttpContextAccessor httpContextAccessor, IApplTaskService applTask)
        {
            applTask = _applTaskService;
            _httpContextAccessor = httpContextAccessor;
            if (_session.IsAvailable)
            {
                if (_session.GetInt32("IdUser") != null)
                {
                    IdUser = (int)_session.GetInt32("IdUser");
                }
            }
        }

        public IList<ApplTaskModel> GetMenus()
        {
            string str = "";
            var ret = _applTaskService.GetMenus(JenisAplikasiConstant.Web, IdUser, out _);
            //if (ret.Count() == 0)
            //    return null;
            allMenuItems = ret;

            return allMenuItems;
        }

        public IViewComponentResult Invoke()
        {
            var stb = new StringBuilder();
            var ret = _applTaskService.GetMenus(JenisAplikasiConstant.Web, IdUser, out _);
            //if (ret.Count() == 0)
            //    return null;
            
            if ((ret.Count() == 0 ) || ret == null)
            {
                stb.Append("<ul> </ul>");
            }
            else
            {
                var baseUrlMenu = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}";
                List<ApplTaskModel> parentItems = (from a in GetMenus() where a.IdApplTaskParent == null select a).ToList();
                stb.Append("<li><a class='nav-link' href='" + baseUrlMenu + "/Home/Index'><i class='fa fa-tachometer'></i> <span>Dashboard</span></a></li>");
                // handle for child used recursive
                foreach (var parentCat in parentItems)
                {
                    stb.Append(OPEN_LIST_ITEM_TAG_CLASS);
                    List<ApplTaskModel> childItems = (from a in allMenuItems where a.IdApplTaskParent == parentCat.IdApplTask select a).ToList();
                    string iconName = parentCat.IconName != null ? parentCat.IconName.ToString() : string.Format("fa fa-bar");
                    if(childItems.Count > 0)
                    {
                        stb.Append("<a href='" + parentCat.ControllerName + "/" + parentCat.ActionName + "'  class='nav-link has-dropdown' data-toggle='dropdown'> <i class='" + iconName + "'></i> <span>" + parentCat.ApplTaskName + "</span> </a>");
                        AddChildItem(parentCat, stb);
                    }
                    else
                    {
                        stb.Append("<a href='javascript:void(0)' data-toggle='dropdown' class='nav-link has-dropdown'> <i class='" + iconName + "'></i> <span>" + parentCat.ApplTaskName + "</span> </a>");
                    }
                }
            }
            // hardcode for menu logout
            stb.Append("<li><a class='nav-link' href='#' onClick='Logout();'><i class='fa fa-sign-out'></i> <span>Logout</span></a></li>");

            ViewBag.html = stb.ToString();
            return View();
        }


        private void AddChildItem(ApplTaskModel childItem, StringBuilder strBuilder)
        {
            strBuilder.Append("<ul class='dropdown-menu'>");
            List<ApplTaskModel> childItems = (from a in allMenuItems where a.IdApplTaskParent == childItem.IdApplTask select a).ToList();
            foreach (ApplTaskModel cItem in childItems)
            {
                if (cItem.ControllerName != null)
                {
                    var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}";

                    strBuilder.Append("<li class=''>");
                    strBuilder.Append("<a href='" + baseUrl + "/" + cItem.ControllerName + "/" + cItem.ActionName + "' class='nav-link'> " + cItem.ApplTaskName + "  </a>");
                    List<ApplTaskModel> subChilds = (from a in allMenuItems where a.IdApplTaskParent == cItem.IdApplTask select a).ToList();
                    if (subChilds.Count > 0)
                    {
                        AddChildItem(cItem, strBuilder);
                    }

                    strBuilder.Append(CLOSE_LIST_ITEM_TAG);
                }
                else
                {
                    strBuilder.Append("<li class='nav-item dropdown'>");
                    strBuilder.Append("<a href='javascript:void(0)' class='nav-link has-dropdown'>  " + cItem.ApplTaskName + "  </a>");
                    List<ApplTaskModel> subChilds = (from a in allMenuItems where a.IdApplTaskParent == cItem.IdApplTask select a).ToList();
                    if (subChilds.Count > 0)
                    {
                        AddChildItem(cItem, strBuilder);
                    }

                    strBuilder.Append(CLOSE_LIST_ITEM_TAG);
                }

            }
            strBuilder.Append(CLOSE_LIST_TAG);
        }

    }
}
