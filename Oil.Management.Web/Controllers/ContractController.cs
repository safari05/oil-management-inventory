using Microsoft.AspNetCore.Mvc;
using Oil.Management.Shared.Interfaces;
using Oil.Management.Shared.ViewModels;
using Oil.Management.Shared.ViewModels.Transaction;
using Oil.Management.Web.Models;

namespace Oil.Management.Web.Controllers
{

    
    public class ContractController : BaseController
    {
        private readonly IMasterService _masterService;
        private readonly ITransaction _transaction;

        private readonly int IdUser;
        private readonly string Token = string.Empty;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public ContractController(IHttpContextAccessor httpContextAccessor, IMasterService masterService, ITransaction iTransaction)
        {
            _masterService = masterService;
            _httpContextAccessor = httpContextAccessor;
            _transaction = iTransaction;
            if (_session.IsAvailable)
            {
                if (_session.GetInt32("IdUser") != null)
                    IdUser = (int)_session.GetInt32("IdUser");
            }
        }
        public IActionResult Index()
        {
            var ApplSetings = ModuleJs("GetContracts");
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

        public IActionResult Edit(int id)
        {
            try
            {
                var ret = _transaction.GetContract(id, out string oMessage);
                if (!string.IsNullOrEmpty(oMessage))
                {
                    TempData["errorMessage"] = $"Contract details not available with the Id :{id}";
                    return RedirectToAction("Index");
                }
                else
                {
                    var ApplSetings = ModuleJs("CreateContract");
                    ViewBag.ModuleJs = ApplSetings;
                    return View(ret);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex;
                return RedirectToAction("Index");
                throw;
            }
        }

        #region Module Js
        public List<AppLibraryModel> ModuleJs(string ModuleName)
        {
            List<AppLibraryModel> ret = new List<AppLibraryModel>();

            AppLibraryModel factorys = new AppLibraryModel()
            {
                Name = "contract_list.js",
                Path = "Transaction/Contract",
                Type = "module"
            };

            AppLibraryModel createFactory = new AppLibraryModel
            {
                Name = "create_contract.js",
                Path = "Transaction/Contract",
                Type = "module"
            };
            if (ModuleName == "GetContracts")
            {
                ret.Add(factorys);
            }
            else if (ModuleName == "CreateContract")
            {
                ret.Add(createFactory);
            }

            return ret;
        }
        #endregion

        #region Access to Services
        public ResponseModel<List<TrxContractModel>> GetContracts()
        {
            var ret = _transaction.GetContracts(out string oMessage);
            return new ResponseModel<List<TrxContractModel>>
            {
                IsSuccess = (string.IsNullOrEmpty(oMessage)) ? true : false,
                Data = ret,
                ReturnMessage = oMessage
            };
        }

        [HttpPost]
        public ResponseModel<string> AddContract(string Name, DateTime StartContract, DateTime EndContract, 
                                                IFormFile fileGuarante, int PctDomestic, int PctExport, string Description, int IdFactory)
        {
            TrxCntractAddModel Data = new TrxCntractAddModel()
            {
                Name = Name,
                StartContract = StartContract,
                FileGuarante = fileGuarante,
                PctDomestic = PctDomestic,
                PctExport = PctExport,
                EndContract = EndContract,
                Description = Description,
                IdFactory = IdFactory

            };
            var ret = _transaction.AddContract(IdUser, Data);
            return new ResponseModel<string>
            {
                IsSuccess = (string.IsNullOrEmpty(ret)) ? true : false,
                Data = ret,
                ReturnMessage = ret
            };
        }

        [HttpPost]
        public ResponseModel<string> EditContract(int Id, string Name, DateTime StartContract, DateTime EndContract,
                                                IFormFile fileGuarante, int PctDomestic, int PctExport, string Description, int IdFactory)
        {
            TrxContractModel Data = new TrxContractModel()
            {
                Id = Id,
                Name = Name,
                StartContract = StartContract,
                FileGuarante = fileGuarante,
                PctDomestic = PctDomestic,
                PctExport = PctExport,
                EndContract = EndContract,
                Description = Description,
                IdFactory = IdFactory

            };

            var ret = _transaction.EditContract(IdUser, Data);
            return new ResponseModel<string>
            {
                IsSuccess = (string.IsNullOrEmpty(ret)) ? true : false,
                Data= ret,
                ReturnMessage = ret
            };
        }


        #endregion
    }
}
