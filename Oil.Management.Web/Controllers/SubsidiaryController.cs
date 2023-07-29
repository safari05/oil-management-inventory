using Microsoft.AspNetCore.Mvc;
using Oil.Management.Shared.Interfaces;
using Oil.Management.Shared.ViewModels;
using Oil.Management.Shared.ViewModels.Master;
using Oil.Management.Web.Models;

namespace Oil.Management.Web.Controllers
{
    public class SubsidiaryController : Controller
    {
        private readonly IMasterService _masterService;

        private readonly int IdUser;
        private readonly string Token = string.Empty;
        private readonly IHttpContextAccessor _httpContextAccessor;

       
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public SubsidiaryController(IHttpContextAccessor httpContextAccessor, IMasterService masterService)
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
            var ApplSetings = ModuleJs("GetSubsidiaryCompany");
            ViewBag.ModuleJs = ApplSetings;
            return View();
        }

        public IActionResult Create()
        {
            var create = ModuleJs("CreateSubsidiary");
            ViewBag.ModuleJs = create;
            return View();
        }

        #region Acces data layer services
        public ResponseModel<List<SubsidiaryCompanyModel>> GetSubsidiarys()
        {
            var ret = _masterService.SubsidiaryCompanys(out string oMessage);
            return new ResponseModel<List<SubsidiaryCompanyModel>>
            {
                IsSuccess = (string.IsNullOrEmpty(oMessage)) ? true : false,
                Data = ret,
                ReturnMessage = oMessage
            };
        }

        public IActionResult Factorys(string factoryname)
        {
            if (factoryname == null)
            {
                var factorys = _masterService.GetFactorys(out string oMessage);
                return new JsonResult(factorys);
            }
            else
            {
                var factorys = _masterService.GetFactoryByNames(factoryname, out string oMessage);
                return new JsonResult(factorys);
            }
        }

        public IActionResult BusniessUnits(string busniessNama)
        {
            
            if(busniessNama == null)
            {
                var busniess = _masterService.GetBusinessUnits(out string oMessage);
                return new JsonResult(busniess);
            }
            else
            {
                var business = _masterService.GetBusinessUnitByNames(busniessNama, out string oMessage);
                return new JsonResult(business);
            }
        }

        public ResponseModel<string> AddSubsidiary([FromBody] SubsidiaryCompanyAdd Data)
        {
            var ret = _masterService.AddSubsidiaryCompany(IdUser, Data);
            return new ResponseModel<string>
            {
                IsSuccess = (string.IsNullOrEmpty(ret)) ? true : false,
                Data = null,
                ReturnMessage = ret
            };
        }

        public ResponseModel<string> EditSubsidiary([FromBody] SubsidiaryCompanyAdd Data)
        {
            var ret = _masterService.EditSubsidiaryCompany(IdUser, Data);
            return new ResponseModel<string>
            {
                IsSuccess = (string.IsNullOrEmpty(ret)) ? true : false,
                Data = null,
                ReturnMessage = ret
            };
        }
        #endregion


        #region Module Js
        public List<AppLibraryModel> ModuleJs(string ModuleName)
        {
            List<AppLibraryModel> ret = new List<AppLibraryModel>();

            AppLibraryModel subsidiary = new AppLibraryModel()
            {
                Name = "subsidiary_companys.js",
                Path = "Master/Subsidiary/",
                Type = "module"
            };


            AppLibraryModel CreateSubsidiary = new AppLibraryModel()
            {
                Name = "subsidiary_companys_create.js",
                Path = "Master/Subsidiary/",
                Type = "module"
            };

            if (ModuleName == "GetSubsidiaryCompany")
            {
                ret.Add(subsidiary);
            }else if(ModuleName == "CreateSubsidiary")
            {
                ret.Add(CreateSubsidiary);
            }
           

            return ret;
        }
        #endregion
    }
}
