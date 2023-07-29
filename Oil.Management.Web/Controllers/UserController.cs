using Microsoft.AspNetCore.Mvc;
using Oil.Management.Shared.Interfaces;
using Oil.Management.Shared.ViewModels;
using Oil.Management.Shared.ViewModels.ApplMgt;
using Oil.Management.Web.Models;

namespace Oil.Management.Web.Controllers
{
    public class UserController : Controller
    {
        public readonly IUserServices _userService;
        private readonly int IdUser;
        private readonly string Token = string.Empty;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public UserController(IUserServices userServices, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userServices;

            _httpContextAccessor = httpContextAccessor;
            if (_session.IsAvailable)
            {
                if (_session.GetInt32("IdUser") != null)
                    IdUser = (int)_session.GetInt32("IdUser");
            }
        }
        public IActionResult Index()
        {
            var ApplSetings = ModuleJs("UserManagement");
            ViewBag.ModuleJs = ApplSetings;
            return View();
        }

        public IActionResult GantiPassword()
        {
            return View();
        }


        public IActionResult Create()
        {
            var ApplSetings = ModuleJs("CreateUser");
            ViewBag.ModuleJs = ApplSetings;
            return View();
        }

        
       

        public ResponseModel<string> AddUser([FromBody] UserAddModel Data)
        {
            string ret = _userService.AddUser(IdUser, Data);
            return new ResponseModel<string>
            {
                IsSuccess = (string.IsNullOrEmpty(ret)) ? true : false,
                ReturnMessage = ret,
                Data = null
            };
        }

        public ResponseModel<string> SetUserActive(int SetIdUser)
        {

            var ret = _userService.SetUserActive(IdUser, SetIdUser, out string oMessage);
            return new ResponseModel<string>
            {
                IsSuccess = string.IsNullOrEmpty(oMessage) ? true : false,
                ReturnMessage = oMessage,
                Data = ret
            };
        }

        public ResponseModel<string> SetUserInActive(int SetIdUser)
        {

            var ret = _userService.SetUserInActive(IdUser, SetIdUser, out string oMessage);
            return new ResponseModel<string>
            {
                IsSuccess = string.IsNullOrEmpty(oMessage) ? true : false,
                ReturnMessage = oMessage,
                Data = ret
            };
        }


        public ResponseModel<List<UserModel>> GetUsers()
        {

            var ret = _userService.GetUsers(out string oMessage);
            return new ResponseModel<List<UserModel>>
            {
                IsSuccess = ret != null,
                ReturnMessage = ret != null ? "" : "data tidak ada",
                Data = ret
            };
        }

        #region Module Js
        public List<AppLibraryModel> ModuleJs(string ModuleName)
        {
            List<AppLibraryModel> ret = new List<AppLibraryModel>();

            AppLibraryModel UserManagement = new AppLibraryModel
            {
                Name = "user_management.js",
                Path = "applmgt",
                Type = "Module"
            };
             AppLibraryModel CreateUser = new AppLibraryModel
            {
                Name = "create_user.js",
                Path = "applmgt",
                Type = "Module"
            };

            if (ModuleName == "UserManagement")
            {
                ret.Add(UserManagement);
            }else if(ModuleName == "CreateUser")
            {
                ret.Add(CreateUser);
            }
            
            return ret;
        }
        #endregion
    }
}
