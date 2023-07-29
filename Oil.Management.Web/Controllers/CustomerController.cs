using Microsoft.AspNetCore.Mvc;
using Oil.Management.Shared.Interfaces;
using Oil.Management.Shared.ViewModels;
using Oil.Management.Shared.ViewModels.Master;
using Oil.Management.Web.Models;

namespace Oil.Management.Web.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly IMasterService _masterService;

        private readonly int IdUser;
        private readonly string Token = string.Empty;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public CustomerController(IHttpContextAccessor httpContextAccessor, IMasterService masterService)
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
            var ApplSetings = ModuleJs("GetCustomers");
            ViewBag.ModuleJs = ApplSetings;
            return View();
        }

        [HttpGet]
        public IActionResult create()
        {
            var ApplSetings = ModuleJs("CreateContract");
            ViewBag.ModuleJs = ApplSetings;
            return View();
        }

        public ResponseModel<List<SubsidiaryCompanyModel>> GetSubsidiarys()
        {
            List<SubsidiaryCompanyModel> ret = _masterService.SubsidiaryCompanys(out string oMessage);
            return new ResponseModel<List<SubsidiaryCompanyModel>>
            {
                IsSuccess = (string.IsNullOrEmpty(oMessage)) ? true : false,
                ReturnMessage = oMessage,
                Data = ret
            };
        }

        #region Acces services layer
        public ResponseModel<List<CustomerModel>> GetCustomers()
        {
            var ret = _masterService.GetCustomers(out string oMessage);
            return new ResponseModel<List<CustomerModel>>
            {
                IsSuccess = (string.IsNullOrEmpty(oMessage)) ? true : false,
                Data = ret,
                ReturnMessage = oMessage
            };
        }

        public ResponseModel<string> AddCustomer([FromBody] CustomerAddModel Data)
        {
            string ret = _masterService.CustomerCreate(IdUser, Data);
            return new ResponseModel<string>
            {
                IsSuccess = (string.IsNullOrEmpty(ret)) ? true : false,
                ReturnMessage = ret,
                Data = null
            };
        }
        #endregion
        #region Module Js
        public List<AppLibraryModel> ModuleJs(string ModuleName)
        {
            List<AppLibraryModel> ret = new List<AppLibraryModel>();

            AppLibraryModel customers = new AppLibraryModel()
            {
                Name = "customer_list.js",
                Path = "Master/Customer/",
                Type = "module"
            };

            AppLibraryModel createCustomer= new AppLibraryModel
            {
                Name = "create_customer.js",
                Path = "Master/Customer",
                Type = "module"
            };
            if (ModuleName == "GetCustomers")
            {
                ret.Add(customers);
            }
            else if (ModuleName == "CreateContract")
            {
                ret.Add(createCustomer);
            }

            return ret;
        }
        #endregion
    }
}
