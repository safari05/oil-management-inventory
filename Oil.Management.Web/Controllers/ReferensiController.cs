using Microsoft.AspNetCore.Mvc;
using Oil.Management.Web.Models;

namespace Oil.Management.Web.Controllers
{
    public class ReferensiController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult type_user()
        {
            var ApplSetings = ModuleJs("TypeUser");
            ViewBag.ModuleJs = ApplSetings;
            return View();
        }


        #region Module JS
        public List<AppLibraryModel> ModuleJs (string ModuleName)
        {
            List<AppLibraryModel> ret = new List<AppLibraryModel>();

            AppLibraryModel typeUser = new AppLibraryModel()
            {
                Name = "type_user.js",
                Path = "Referensi",
                Type = "module"
            };

            if(ModuleName == "TypeUser")
            {
                ret.Add(typeUser);
            }

            return ret;
        }
        #endregion
    }
}
