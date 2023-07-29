using Dapper;
using Oil.Management.Entities.ApplMgt;
using Oil.Management.Entities.Master;
using Oil.Management.Entities.Transaction;
using Oil.Management.Shared;
using Oil.Management.Shared.Constants;
using Oil.Management.Shared.Interfaces;
using Oil.Management.Shared.ViewModels.ApplMgt;
using Oil.Management.Shared.ViewModels.Transaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Oil.Management.Services
{
    public class TransactionService : ITransaction
    {

        private readonly string ServiceName = "Transaction.Services.TransactionService.";
        private readonly Common common = new Common();

        public ApproveModelRole GetApproveFinance(int IdUser, out string oMessage)
        {
            oMessage = string.Empty;
            try
            {

                using (IDbConnection conn = common.DbConnection)
                {
                    conn.Open();
                    var user = (from a in conn.GetList<TbUser>()
                                where a.IdUser == IdUser
                                select a).FirstOrDefault();

                    //var isApproveFinance = ApproveFinanceRoleConstant.DictRoleFinance.Where(p =>p.Key == user.IdRole).ToDictionary(p => p.Key, p => p.Value);
                    var getApproveFinance = (from a in ApproveFinanceRoleConstant.DictRoleFinance
                                             where a.Key == user.IdRole
                                             select new ApproveModelRole
                                             {
                                                 Id = a.Key,
                                                 Name = a.Value
                                             }).FirstOrDefault();

                    if (getApproveFinance == null)
                    {
                        oMessage = "Not found";
                        return null;
                    }
                    else
                    {
                        ApproveModelRole mod = new ApproveModelRole();
                        mod.Id = getApproveFinance.Id;
                        mod.Name = getApproveFinance.Name;
                        return mod;
                    }

                   

                }
            }
            catch (Exception ex)
            {
                common.GetErrorMessage(ServiceName + "Is Approve Finance", ex);
                return null;
            }
        }
        public string AddContract(int IdUser, TrxCntractAddModel Data)
        {
            try
            {
                if(Data == null)
                {
                    return "Column Required";
                }
                using (var transactionScope = new TransactionScope())
                {
                    using(IDbConnection conn = common.DbConnection)
                    {
                        conn.Open();
                        using(var tx = conn.BeginTransaction())
                        {

                            TrxContract contract = new TrxContract();
                            contract.NameContract = Data.Name;
                            contract.IdFactory = Data.IdFactory;
                            contract.StartContract = Data.StartContract;
                            contract.EndContract = Data.EndContract;
                            contract.PctDomestic = Data.PctDomestic;
                            contract.PctEkspor = Data.PctExport;
                            contract.Status = 0;
                            contract.Description= Data.Description;
                            contract.CreatedBy = IdUser;
                            contract.CreatedDt = DateTime.Now;
                            contract.FileGuaranteArchive = String.Format("guarante_{0}_{1}{2}", Data.Name.Replace(" ", "_"), DateTime.Now.Year.ToString(), Path.GetExtension(Data.FileGuarante.FileName));

                            var directory = ConstPathFilesConstant.PathFileGuaranteUpload;

                            if (!System.IO.Directory.Exists(directory))
                            {
                                System.IO.Directory.CreateDirectory(directory);
                            }

                            using (var fileStream = new FileStream(Path.Combine(ConstPathFilesConstant.PathFileGuaranteUpload, contract.FileGuaranteArchive), FileMode.Create))
                            {
                                Data.FileGuarante.CopyTo(fileStream);
                            }
                            conn.Insert(contract);
                            tx.Commit();
                            transactionScope.Complete();

                            return String.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return common.GetErrorMessage(ServiceName + "AddContract", ex);
            }
        }

        public string AddPurchaseOrder(int IdUser, TrxCompanyPurchaseOrderAddModel Data)
        {
            try
            {
                if (Data == null)
                {
                    return "Column Required";
                }
                using (var transactionScope = new TransactionScope())
                {
                    using (IDbConnection conn = common.DbConnection)
                    {
                        conn.Open();
                        using (var tx = conn.BeginTransaction())
                        {

                            TrxPurchaseOrder po = new TrxPurchaseOrder();
                            po.CodePo = Data.CodePo;
                            po.NamePo = Data.PoName;
                            po.TotalOrder = Data.TotalOrder;
                            po.Satuan = Data.SatuanVolume;
                            po.IdContract = Data.IdContract;
                            po.CreatedBy = IdUser;
                            po.CreatedDt = DateTime.Now;

                            conn.Insert(po);
                            tx.Commit();
                            transactionScope.Complete();
                            return String.Empty;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                return common.GetErrorMessage(ServiceName + "AddPurchaseOrder", ex);
            }
        }

        public string ApprovePurchaseOrder(int IdUser, TrxCompanyPurchaseOrderApproveModel Data)
        {
            try
            {
                using (var transactionScope = new TransactionScope())
                {

                    using (IDbConnection conn = common.DbConnection)
                    {
                        conn.Open();
                        using (var tx = conn.BeginTransaction())
                        {

                            var po = (from a in conn.GetList<TrxPurchaseOrder>()
                                            where a.IdPo == Data.IdTrxPo
                                            select a).FirstOrDefault();

                            if (po == null)
                            {
                                return "Data not found";
                            }

                            po.Status = Data.Status;
                            po.Amount = Data.Amount;
                            po.UpdatedBy = IdUser;
                            po.UpdatedDt = DateTime.Now;
                            po.TimeArrival = Data.TimeArival;
                            conn.Update(po);

                            TrxPurchaseOrderStatus tpos = new TrxPurchaseOrderStatus();
                            tpos.IdPurchaseOrder = po.IdPo;
                            
                            tpos.Status = Data.Status;
                            tpos.DescriptionStatus = Data.Description;
                            tpos.CreatedBy = IdUser;
                            tpos.CreatedDt = DateTime.Now;


                            conn.Insert(tpos);

                            if (Data.Status == StatusTransactionConstant.Approve)
                            {

                                var getPaymentCompany = (from a in conn.GetList<TrxPaymentCompany>()
                                                         where a.IdPo == po.IdPo
                                                         select a).FirstOrDefault();

                                if (getPaymentCompany == null )
                                {
                                    TrxPaymentCompany tpc = new TrxPaymentCompany();
                                    tpc.IdPo = Data.IdTrxPo;
                                    tpc.AmountBill = po.Amount;
                                    tpc.AmountDifference = po.Amount;
                                    tpc.Status = StatusTransactionPaymentConstant.Initiated;

                                    conn.Insert(tpc);
                                }
                                else
                                {
                                    getPaymentCompany.AmountBill = po.Amount;
                                    getPaymentCompany.AmountDifference = po.Amount;
                                    conn.Update(getPaymentCompany);
                                }

                                var companyRefinery = (from a in conn.GetList<TmCompanyRefinery>()
                                                         where a.IdPurchase == po.IdPo
                                                         select a).FirstOrDefault();

                                if (companyRefinery == null)
                                {
                                    TmCompanyRefinery tpcRefinery = new TmCompanyRefinery()
                                    {
                                        IdCompany = 1,
                                        DateRecieve = Data.TimeArival,
                                        IdPurchase = po.IdPo,
                                        Stock = po.TotalOrder,

                                    };
                                    conn.Insert(tpcRefinery);
                                }
                                else
                                {
                                    companyRefinery.DateRecieve = Data.TimeArival;
                                    companyRefinery.Stock = po.TotalOrder;
                                    conn.Update(companyRefinery);
                                }
                                
                            }

                           
                            tx.Commit();
                            transactionScope.Complete();
                        }
                    }
                    return string.Empty;

                }
            }
            catch (Exception ex)
            {
                return common.GetErrorMessage(ServiceName + "EditContract", ex);
            }
        }

        public string EditContract(int IdUser, TrxContractModel Data)
        {
            try
            {
                using (var transactionScope = new TransactionScope())
                {

                    using(IDbConnection conn = common.DbConnection)
                    {
                        conn.Open();
                        using(var tx = conn.BeginTransaction())
                        {

                            var contract = (from a in conn.GetList<TrxContract>()
                                             where a.IdContract == Data.Id select a).FirstOrDefault();

                            if (contract == null)
                            {
                                return "Data not found";
                            }
                            
                            contract.NameContract = Data.Name;
                            contract.IdFactory = Data.IdFactory;
                            contract.StartContract = Data.StartContract;
                            contract.EndContract = Data.EndContract;
                            contract.PctDomestic = Data.PctDomestic;
                            contract.PctEkspor = Data.PctExport;
                            contract.Description = Data.Description;
                            contract.UpdatedBy = IdUser;
                            contract.UpdatedDt = DateTime.Now;
                            

                            if (Data.FileGuarante != null)
                            {
                                if (File.Exists(Path.Combine(ConstPathFilesConstant.PathFileGuaranteUpload, contract.FileGuaranteArchive)))
                                    File.Delete(Path.Combine(ConstPathFilesConstant.PathFileGuaranteUpload, contract.FileGuaranteArchive));

                                var directory = ConstPathFilesConstant.PathFileGuaranteUpload;
                                if (!System.IO.Directory.Exists(directory))
                                {
                                    System.IO.Directory.CreateDirectory(directory);
                                }

                                using (var fileStream = new FileStream(Path.Combine(ConstPathFilesConstant.PathFileGuaranteUpload, contract.FileGuaranteArchive), FileMode.Create))
                                {
                                    Data.FileGuarante.CopyTo(fileStream);
                                }
                                

                            }
                            conn.Update(contract);
                            tx.Commit();
                            transactionScope.Complete();
                        }
                    }
                    return string.Empty;

                }
            }
            catch (Exception ex)
            {
                return common.GetErrorMessage(ServiceName + "EditContract", ex);
            }
        }

        public string EditPurchaseOrder(int IdUser, TrxCompanyPurchaseOrderModel Data)
        {
            throw new NotImplementedException();
        }

        public List<TrxCompanyPurchaseOrderModel> GetCompanyPurchaseOrders(int IdUser,out string oMessage)
        {
            
            oMessage = string.Empty;
            List<TrxCompanyPurchaseOrderModel> result = new List<TrxCompanyPurchaseOrderModel>();
            try
            {
                using(IDbConnection conn = common.DbConnection)
                {
                    var getApprove = this.GetApproveFinance(IdUser, out string oMessages);
                    bool isApprove = false;
                    if (getApprove != null)
                    {
                        isApprove = true;
                    }
                    var poCompanys = (from a in conn.GetList<TrxPurchaseOrder>()
                                     join b in conn.GetList<TrxContract>() on a.IdContract equals b.IdContract
                                     join c in conn.GetList<TbUser>() on a.CreatedBy equals c.IdUser
                                     select new {a, b, c}).ToList();

                    if(poCompanys.Count == 0)
                    {
                        oMessage = "Data not found";
                    }

                    foreach(var item in poCompanys)
                    {
                        var dt = (DateTime?)(item.a.TimeArrival == null ? (DateTime?)null : (DateTime)item.a.TimeArrival);
                        TrxCompanyPurchaseOrderModel mod = new TrxCompanyPurchaseOrderModel();
                        mod.IdTrxPo = item.a.IdPo;
                        mod.ContractName = item.b.NameContract;
                        mod.PoName = item.a.NamePo;
                        mod.CodePo = item.a.CodePo;
                        mod.StrStatus = StatusTransactionConstant.DictStatusTransaction[item.a.Status];
                        mod.CreatedBy = item.c.Username;
                        mod.Amount = item.a.Amount;
                        mod.Total = item.a.TotalOrder;
                        mod.Tax = item.a.Tax;
                        mod.TimeArival = dt;
                        mod.IsApprove = isApprove;
                        result.Add(mod);
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                oMessage = common.GetErrorMessage(ServiceName + "", ex);
                return null;
            }
        }

        public TrxContractModel GetContract(int IdContract, out string oMessage)
        {
            oMessage = String.Empty;
            try
            {
                using(IDbConnection conn= common.DbConnection)
                {
                    var contract = (from a in conn.GetList<TrxContract>()
                                    join b in conn.GetList<TbMFactoryOil>() on a.IdFactory equals b.IdFactory
                                    join c in conn.GetList<TbUser>() on a.CreatedBy equals c.IdUser
                                    select new { a, b, c }).FirstOrDefault();
                    if (contract == null)
                    {
                        oMessage = " record not found";
                    }
                    return new TrxContractModel
                    {
                        Id = contract.a.IdContract,
                        Name = contract.a.NameContract,
                        StartContract= contract.a.StartContract,
                        EndContract= contract.a.EndContract,
                        Status = StatusDataConstant.DictStatusData[contract.a.Status],
                        IdFactory = contract.a.IdFactory,
                        Description = contract.a.Description,
                        FileGuaranteStr = contract.a.FileGuaranteArchive,
                        PctDomestic = contract.a.PctDomestic,
                        PctExport = contract.a.PctEkspor,
                        FactoryName = contract.b.FactoryName,
                        CreatedBy = contract.c.FirstName,

                    };
                }
            }
            catch (Exception ex)
            {
                common.GetErrorMessage(ServiceName + "GetContract", ex);
                return null;
            }
        }

        public List<TrxContractModel> GetContracts(out string oMessage)
        {
            oMessage = String.Empty;
            List<TrxContractModel> rets = new List<TrxContractModel>();

            try
            {
                using (var transactionScope = new TransactionScope())
                {
                    using (IDbConnection conn = common.DbConnection)
                    {

                        var contracts = (from a in conn.GetList<TrxContract>()
                                         join b in conn.GetList<TbMFactoryOil>() on a.IdFactory equals b.IdFactory
                                         join c in conn.GetList<TbUser>() on a.CreatedBy equals c.IdUser
                                         select new { a, b, c }).ToList();

                        if(contracts.Count == 0)
                        {
                            oMessage = "Data not found";
                        }

                        foreach (var item in contracts)
                        {
                            TrxContractModel mod = new TrxContractModel();
                            mod.Id = item.a.IdContract;
                            mod.Name = item.a.NameContract;
                            mod.StartContract = item.a.StartContract;
                            mod.EndContract = item.a.EndContract;
                            mod.FileGuaranteStr = item.a.FileGuaranteArchive;
                            mod.PctDomestic = item.a.PctDomestic;
                            mod.PctExport = item.a.PctEkspor;
                            mod.Status = StatusDataConstant.DictStatusData[item.a.Status];
                            mod.Description = item.a.Description;
                            mod.CreatedBy = item.c.Username;
                            mod.FactoryName = item.b.FactoryName;


                            rets.Add(mod);
                        }

                        transactionScope.Complete();
                        return rets;
                    }
                }
                                
            }
            catch (Exception ex)
            {
                oMessage = common.GetErrorMessage(ServiceName + "", ex);
                return null;
            }
        }

        public TrxCompanyPurchaseOrderModel GetCompanyPurchaseOrder(int IdTrxCompanyPo, out string oMessage)
        {
            oMessage = String.Empty;
            try
            {
                using (IDbConnection conn = common.DbConnection)
                {
                    var poCompany = (from a in conn.GetList<TrxPurchaseOrder>()
                                      join b in conn.GetList<TrxContract>() on a.IdContract equals b.IdContract
                                      join c in conn.GetList<TbUser>() on a.CreatedBy equals c.IdUser
                                      where a.IdPo == IdTrxCompanyPo
                                      select new { a, b, c }).FirstOrDefault();
                    if (poCompany == null)
                    {
                        oMessage = " record not found";
                    }
                    var timeArival = (DateTime?)(poCompany.a.TimeArrival == null ? (DateTime?)null : (DateTime)poCompany.a.TimeArrival);
                    return new TrxCompanyPurchaseOrderModel
                    {
                       IdTrxPo = poCompany.a.IdPo,
                       IdContract = poCompany.a.IdContract,
                       Amount = poCompany.a.Amount,
                       CodePo = poCompany.a.CodePo,
                       ContractName = poCompany.b.NameContract,
                       SatuanVolume = poCompany.a.Satuan,
                       PoName = poCompany.a.NamePo,
                       Tax = poCompany.a.Tax,
                       TimeArival = timeArival,
                       Total = poCompany.a.TotalOrder,
                       

                    };
                }
            }
            catch (Exception ex)
            {
                common.GetErrorMessage(ServiceName + "GetContract", ex);
                return null;
            }
        }
    }
}
