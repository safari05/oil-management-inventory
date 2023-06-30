using Microsoft.AspNetCore.Mvc;
using Oil.Management.Shared.Interfaces;
using Oil.Management.Shared.ViewModels;
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
        public IActionResult Index()
        {
            return View();
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



            if(moduleName == "MenuSetting")
            {
                ret.Add(menuApp);
            }


            return ret;
        }
        #endregion
    }
}
