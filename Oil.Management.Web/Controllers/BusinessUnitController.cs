using Microsoft.AspNetCore.Mvc;
using Oil.Management.Shared.Interfaces;
using Oil.Management.Shared.ViewModels;
using Oil.Management.Shared.ViewModels.Reference;
using Oil.Management.Web.Models;

namespace Oil.Management.Web.Controllers
{
    public class BusinessUnitController : BaseController
    {

        private readonly IMasterService _masterService;
        private readonly int IdUser;
        private readonly string Token = string.Empty;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public BusinessUnitController(IHttpContextAccessor httpContextAccessor, IMasterService masterService)
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
            var _businessUninit = ModuleJs("BusinessUnit");
            ViewBag.ModuleJs = _businessUninit;
            return View();
        }

        [HttpGet("create", Name ="create")]
        public IActionResult Create()
        {
            var addOrUpdateBusinessUnit = ModuleJs("CreateOrUpdate");
            ViewBag.ModuleJs = addOrUpdateBusinessUnit;
            return View();
        }

        public IActionResult Edit(int id)
        {
            try
            {
                var ret = _masterService.GetBusinessUnit(id, out string oMessage);
                if (!string.IsNullOrEmpty(oMessage))
                {
                    TempData["errorMessage"] = $"Business Unit details not available with the Id :{id}";
                    return RedirectToAction("Index");
                }
                else
                {
                    var ApplSetings = ModuleJs("CreateOrUpdate");
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


        #region Acces data layer services

        //[HttpGet("GetBusinessUnits", Name = "GetBusinessUnits")]
        public ResponseModel<List<BusinessUnitModel>> GetBusinessUnits()
        {
            var ret = _masterService.GetBusinessUnits(out string oMessage);
            return new ResponseModel<List<BusinessUnitModel>>
            {
                IsSuccess = (string.IsNullOrEmpty(oMessage)) ? true : false,
                Data = ret,
                ReturnMessage = oMessage
            };
        }

        public ResponseModel<string> AddBusinessUnit ([FromBody] BusinessUnitAddModel Data)
        {
            var ret = _masterService.AddBussinessUnit(IdUser, Data);
            return new ResponseModel<string>
            {
                IsSuccess = (string.IsNullOrEmpty(ret)) ? true : false,
                Data = null,
                ReturnMessage = ret
            };
        }

        public ResponseModel<string> EditBusinessUnit ([FromBody] BusinessUnitModel Data)
        {
            var ret = _masterService.EditBusiness(IdUser, Data);
            return new ResponseModel<string>
            {
                IsSuccess= (string.IsNullOrEmpty(ret)) ? true : false,
                Data= ret,
                ReturnMessage= ret
            };
        }
        #endregion




        #region Module JS
        public List<AppLibraryModel> ModuleJs(string ModuleName)
        {
            List<AppLibraryModel> ret = new List<AppLibraryModel>();

            AppLibraryModel businessUnit = new AppLibraryModel()
            {
                Name = "business_unit_list.js",
                Path = "Referensi/BusniessUnit/",
                Type = "module"
            };
            
            AppLibraryModel businessUnitSave = new AppLibraryModel()
            {
                Name = "business_unit_add_or_update.js",
                Path = "Referensi/BusniessUnit/",
                Type = "module"
            };

            if (ModuleName == "BusinessUnit")
            {
                ret.Add(businessUnit);
            }
            else
            {
                ret.Add(businessUnitSave);
            }

            return ret;
        }
        #endregion
    }
}
