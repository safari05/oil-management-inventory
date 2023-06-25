using Microsoft.AspNetCore.Mvc;
using Oil.Management.Shared.Interfaces;
using Oil.Management.Shared.ViewModels;
using Oil.Management.Shared.ViewModels.ApplMgt;
using Oil.Management.Web.Models;

namespace Oil.Management.Web.Controllers
{
    public class SignController : Controller
    {
        public readonly IConfiguration _configuration;
        private readonly string apiBaseUrl;
        private readonly IUserServices _userServices;

        public SignController(IConfiguration Configuration, IUserServices userServices)
        {
            _configuration = Configuration;
            apiBaseUrl = Configuration.GetValue<string>("WebAPIBaseUrl");
            _userServices = userServices;
        }

        public IActionResult Index()
        {
            var SignJs = ModuleJs("Sign");
            ViewBag.ModuleJs = SignJs;
            return View();
        }


        public ResponseModel<UserModel> Login (string UsernameOrEmail, string Password)
        {
            UserModel ret = _userServices.Login(UsernameOrEmail, Password, out string oMessage);
            bool isSuccess = (string.IsNullOrEmpty(oMessage)) ? true:false;

            if (isSuccess)
            {

                // store to session
                HttpContext.Session.SetInt32("UserIdOrUsernamOrEmail", ret.IdUser);
                HttpContext.Session.SetInt32("IdUser", ret.IdUser);
                HttpContext.Session.SetString("Username", ret.UserName);
                HttpContext.Session.SetString("NamaPengguna", ret.FirstName);
                return new ResponseModel<UserModel>
                {
                    IsSuccess = false,
                    ReturnMessage = oMessage,
                    Data = ret
                };
            }
            else
            {
                return new ResponseModel<UserModel>
                {
                    IsSuccess = false,
                    ReturnMessage = oMessage,
                    Data = ret
                };
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("Index");
        }



        #region Load Diference JS File Module
        public List<AppLibraryModel> ModuleJs(string ModuleName)
        {
            List<AppLibraryModel> ret = new List<AppLibraryModel>();

            AppLibraryModel Signs = new AppLibraryModel
            {
                Name = "sign.js",
                Path = "Auth",
                Type = "Module"
            };

            if (ModuleName == "Sign")
            {
                ret.Add(Signs);
            }

            return ret;


        }
        #endregion
    }
}
