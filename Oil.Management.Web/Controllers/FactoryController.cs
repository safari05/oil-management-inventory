using Microsoft.AspNetCore.Mvc;
using Oil.Management.Shared.Interfaces;
using Oil.Management.Shared.ViewModels;
using Oil.Management.Shared.ViewModels.Master;
using Oil.Management.Web.Models;

namespace Oil.Management.Web.Controllers
{
    public class FactoryController : BaseController
    {
        private readonly IMasterService _masterService;

        private readonly int IdUser;
        private readonly string Token = string.Empty;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public FactoryController(IHttpContextAccessor httpContextAccessor,IMasterService masterService)
        {
            _masterService = masterService;
            _httpContextAccessor = httpContextAccessor;
            if (_session.IsAvailable)
            {
                if (_session.GetInt32("IdUser") != null)
                    IdUser = (int)_session.GetInt32("IdUser");
            }
        }
        public IActionResult Index()
        {
            var ApplSetings = ModuleJs("GetFactorys");
            ViewBag.ModuleJs = ApplSetings;
            return View();
        }

        [HttpGet]
        public IActionResult create()
        {
            var ApplSetings = ModuleJs("CreateFactory");
            ViewBag.ModuleJs = ApplSetings;
            return View();
        }

        
        public IActionResult edit(int id)
        {
            try
            {
                var ret = _masterService.GetFactory(id, out string oMessage);
                if (!string.IsNullOrEmpty(oMessage))
                {

                    TempData["errorMessage"] = $"Factory details not available with the Id :{id}";
                    return RedirectToAction("Index");
                }
                else
                {
                    var ApplSetings = ModuleJs("CreateFactory");
                    ViewBag.ModuleJs = ApplSetings;
                    return View(ret);
                }
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex;
                return RedirectToAction("Index");
            }
           
        }

        #region Module Js
        public List<AppLibraryModel> ModuleJs(string ModuleName)
        {
            List<AppLibraryModel> ret = new List<AppLibraryModel>();

            AppLibraryModel factorys = new AppLibraryModel()
            {
                Name = "factory.js",
                Path = "Master",
                Type = "module"
            };

            AppLibraryModel createFactory = new AppLibraryModel
            {
                Name = "create_factory.js",
                Path = "Master",
                Type = "module"
            };
            if (ModuleName == "GetFactorys")
            {
                ret.Add(factorys);
            }else if(ModuleName == "CreateFactory")
            {
                ret.Add(createFactory);
            }

            return ret;
        }
        #endregion


        #region Access to services
        public ResponseModel<List<FactoryModel>> GetFactorys()
        {
            var ret = _masterService.GetFactorys(out string oMessage);
            return new ResponseModel<List<FactoryModel>>
            {
                IsSuccess = (string.IsNullOrEmpty(oMessage)) ? true : false,
                Data = ret,
                ReturnMessage = oMessage
            };
        }

        public ResponseModel<string> AddFactory([FromBody] FactoryAddModel Data)
        {
            var ret = _masterService.AddFactory(IdUser, Data);
            return new ResponseModel<string>
            {
                IsSuccess = (string.IsNullOrEmpty(ret)) ? true : false,
                Data = null,
                ReturnMessage = ret
            };
        }

        public ResponseModel<string> EditFactory([FromBody] FactoryModel Data)
        {
            var ret = _masterService.EditFactory(IdUser, Data);
            return new ResponseModel<string>
            {
                IsSuccess = (string.IsNullOrEmpty(ret)) ? true : false,
                Data = null,
                ReturnMessage = ret
            };
        } 

        #endregion
    }
}
