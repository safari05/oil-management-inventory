using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Shared.ViewModels.Transaction
{
    public class TrxCompanyPurchaseOrderModel : TrxCompanyPurchaseOrderAddModel
    {
        public int IdTrxPo { get; set; }
        public string ContractName { get; set; }
        public string StrStatus { get; set; }

        public double Amount { get;set; }
        public double Total { get; set; }

        public double Tax { get; set; }
        public string StrTimeArival { get; set; }
        public DateTime? TimeArival { get; set; }
        public string CreatedBy { get; set; }

        public bool IsApprove { get; set; }



    }

    public class TrxCompanyPurchaseOrderAddModel
    {
        public string CodePo { get; set; }
        public string PoName { get;set; }
        public double TotalOrder { get; set; }  
        public int IdContract { get;set; }  
        public string SatuanVolume { get; set; }


    }

    public class TrxCompanyPurchaseOrderApproveModel
    {
        public int IdTrxPo { get; set; }
        public double Amount { get;set; }   
        public DateTime TimeArival { get; set; }    

        public int Status { get; set; }
        public string StrStatus { get; set; }  
        public string Description { get; set; }
    }
}
