﻿using Microsoft.AspNetCore.Mvc;
using Oil.Management.Shared.Interfaces;
using Oil.Management.Shared.ViewModels;
using Oil.Management.Shared.ViewModels.ApplMgt;
using Oil.Management.Web.Models;

namespace Oil.Management.Web.Controllers
{

    public class ApplmgtController : BaseController
    {
        public readonly IConfiguration _configuration;
        private readonly string apiBaseUrl;
        public readonly IApplTaskService _applTaskService;

        public ApplmgtController(IConfiguration configuration, IApplTaskService applTaskService)
        {
            _configuration = configuration;
            _applTaskService = applTaskService;
        }

        public IActionResult menu_setting()
        {
            var ApplSetings = ModuleJs("MenuSetting");
            ViewBag.ModuleJs = ApplSetings;
            return View();
        }

        public IActionResult user_management()
        {
            var ApplSetings = ModuleJs("UserManagement");
            ViewBag.ModuleJs = ApplSetings;
            return View();
        }

        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetApplParentMenu()
        {
            int id = 1;
            var ret = _applTaskService.GetApplTasks(id, out string oMessage);
            ResponseModel<List<ApplTaskModel>> result = new ResponseModel<List<ApplTaskModel>>
            {
                IsSuccess = (string.IsNullOrEmpty(oMessage)) ? true : false,
                ReturnMessage = oMessage,
                Data = ret
            };
            return Ok(result);
        }

        public IActionResult GetApplMgtTask(int IdAppl)
        {
            string endPoint = apiBaseUrl + "ApplMgts/GetApplMgtTask?";
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            ReqDataTableModel reqPost = new ReqDataTableModel()
            {
                PageStart = skip,
                RecordPerPage = pageSize
            };

            var ret = _applTaskService.GetApplTasks(IdAppl, reqPost, out string oMessage);
            ResponseModel<ApplTaskListModel> mod = new ResponseModel<ApplTaskListModel>();
            if (ret != null)
            {
                var JsonData = new { draw = draw, recordsFiltered = ret.RecordCount, recordsTotal = ret.RecordCount, data = ret.Data };

                return Ok(JsonData);
            }
            else
            {
                var JsonData = new { draw = draw, recordsFiltered = 0, recordsTotal = 0, data = "" };

                return Ok(JsonData);
            }



        }





        #region Setup initilize for js
        public List<AppLibraryModel> ModuleJs(string moduleName)
        {
            List<AppLibraryModel> ret = new List<AppLibraryModel>();

            AppLibraryModel menuApp = new AppLibraryModel()
            {
                Name = "menu_setting.js",
                Path = "applmgt",
                Type = "Module"
            };

            AppLibraryModel UserManagement = new AppLibraryModel
            {
                Name = "user_management.js",
                Path = "applmgt",
                Type = "Module"
            };


            if (moduleName == "MenuSetting")
            {
                ret.Add(menuApp);
            }else if(moduleName == "UserManagement")
            {
                ret.Add(UserManagement);
            }


            return ret;
        }
        #endregion
    }
}
