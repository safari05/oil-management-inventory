using Microsoft.AspNetCore.Mvc;
using Oil.Management.Shared.Constants;
using Oil.Management.Shared.Interfaces;
using Oil.Management.Shared.ViewModels;
using Oil.Management.Shared.ViewModels.Transaction;
using Oil.Management.Web.Models;

namespace Oil.Management.Web.Controllers
{
    public class CompanyPo : BaseController
    {
        private readonly IMasterService _masterService;
        private readonly IUserServices _userServices;
        private readonly ITransaction _transaction;
        private readonly IApplReferenceService _applReferenceService;

        private readonly int IdUser;
        private readonly string Token = string.Empty;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public CompanyPo(ITransaction transaction, IHttpContextAccessor httpContextAccessor, IApplReferenceService applReferenceService)
        {
            _httpContextAccessor = httpContextAccessor;
            _applReferenceService = applReferenceService;
            _transaction = transaction;
            if (_session.IsAvailable)
            {
                if (_session.GetInt32("IdUser") != null)
                    IdUser = (int)_session.GetInt32("IdUser");
            }
        }
        public IActionResult Index()
        {
            var getApprove = _transaction.GetApproveFinance(IdUser, out string oMessages);
            int isApprove;
            if (getApprove == null)
            {
                ViewBag.Approve = false;
                isApprove = 0;
            }
            else
            {
                ViewBag.Approval = true;
                isApprove = 1;
            }
            ViewData["approveFinnace"] = isApprove;
            var ApplSetings = ModuleJs("CompanyPoList");
            ViewBag.ModuleJs = ApplSetings;
            return View();
        }

        public IActionResult Approve(int id)
        {
            try
            {
                var ret = _transaction.GetCompanyPurchaseOrder(id, out string oMessage);
                if (ret == null)
                {
                    TempData["errorMessage"] = $"Po details not available with the Id :{id}";
                    return RedirectToAction("Index");
                }
                else
                {
                    var GetContracts = _transaction.GetContracts(out string oMessage1);
                    if (GetContracts.Count > 0)
                    {
                        ViewBag.Contracts = GetContracts;
                    }
                    else
                    {
                        ViewBag.Contracts = null;
                    }
                    var GetSatuan = _applReferenceService.GetSatuanVolume();
                    var GetStatusTr = _applReferenceService.GetStatusTransactionDict();
                    ViewBag.Satuans = GetSatuan;
                    ViewBag.Contract = ret.IdContract;
                    ViewBag.StatusTransaction = GetStatusTr;
                    ViewBag.SatuanVolume = ret.SatuanVolume;
                    

                    var ApplSetings = ModuleJs("CompanyPoCreate");
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



        public IActionResult Edit (int id)
        {
            return View();
        }



        public IActionResult Create()
        {
            // get satuan contract
            var GetSatuan = _applReferenceService.GetSatuanVolume();

            ViewBag.Satuans = GetSatuan;
           
            // get contract
            var GetContracts = _transaction.GetContracts(out string oMessage);
            if (GetContracts.Count > 0)
            {
                ViewBag.Contracts = GetContracts;
            }
            else
            {
                ViewBag.Contracts = null;
            }

            var ApplSetings = ModuleJs("CompanyPoCreate");
            ViewBag.ModuleJs = ApplSetings;
            return View();
        }

        [HttpPost]
        public ResponseModel<string> AddCompanyPo (TrxCompanyPurchaseOrderAddModel Data)
        {
            var ret = _transaction.AddPurchaseOrder(IdUser, Data);
            return new ResponseModel<string>
            {
                IsSuccess = (string.IsNullOrEmpty(ret)) ? true : false,
                Data = ret,
                ReturnMessage = ret
            };
        }

        [HttpPost]
        public ResponseModel<string> ApproveOrRejectCompanyPo(TrxCompanyPurchaseOrderApproveModel Data)
        {
            var ret = _transaction.ApprovePurchaseOrder(IdUser, Data);
            return new ResponseModel<string>
            {
                IsSuccess = (string.IsNullOrEmpty(ret)) ? true : false,
                Data = ret,
                ReturnMessage = ret
            };
        }


        public ResponseModel<List<TrxCompanyPurchaseOrderModel>> GetPoCompanys()
        {
            var ret = _transaction.GetCompanyPurchaseOrders(IdUser,out string oMessage);
            return new ResponseModel<List<TrxCompanyPurchaseOrderModel>>
            {
                IsSuccess = (string.IsNullOrEmpty(oMessage)) ? true : false,
                Data = ret,
                ReturnMessage = oMessage
            };
        }


        #region Module js
        public List<AppLibraryModel> ModuleJs(string ModuleName)
        {
            List<AppLibraryModel> ret = new List<AppLibraryModel>();
            AppLibraryModel companyPosList = new AppLibraryModel
            {
                Name = "purchase_order_list.js",
                Path = "Transaction/Company",
                Type = "module"
            };

            AppLibraryModel companyPosCreate = new AppLibraryModel
            {
                Name = "purchase_order_create.js",
                Path = "Transaction/Company",
                Type = "module"
            };


            if (ModuleName == "CompanyPoList")
            {
                ret.Add(companyPosList);
            }else if(ModuleName == "CompanyPoCreate")
            {
                ret.Add(companyPosCreate);
            }

            return ret;
        }
    }
    #endregion
}
