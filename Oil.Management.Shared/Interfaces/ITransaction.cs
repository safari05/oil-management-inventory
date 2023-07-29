using Oil.Management.Shared.ViewModels.ApplMgt;
using Oil.Management.Shared.ViewModels.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Shared.Interfaces
{
    public interface ITransaction
    {
        ApproveModelRole GetApproveFinance(int IdUser, out string oMessage);
        List<TrxContractModel> GetContracts(out string oMessage);
        TrxContractModel GetContract(int IdContract, out string oMessage);
        string AddContract(int IdUser, TrxCntractAddModel Data);
        string EditContract(int IdUser, TrxContractModel Data);

        List<TrxCompanyPurchaseOrderModel> GetCompanyPurchaseOrders(int IdUser, out string oMessage);

        TrxCompanyPurchaseOrderModel GetCompanyPurchaseOrder(int IdTrxCompanyPo, out string oMessage);

        string AddPurchaseOrder (int IdUser, TrxCompanyPurchaseOrderAddModel Data);

        string EditPurchaseOrder (int IdUser, TrxCompanyPurchaseOrderModel Data);

        string ApprovePurchaseOrder(int IdUser, TrxCompanyPurchaseOrderApproveModel Data);

    }
}
