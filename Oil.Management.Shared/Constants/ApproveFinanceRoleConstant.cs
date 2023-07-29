using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Shared.Constants
{
    public class ApproveFinanceRoleConstant
    {
        public static Dictionary<int, string> DictRoleFinance = new Dictionary<int, string>
        {
            {FinanceCompany,"Finance Company"},
            {FinanceSubsidiaryCompany,"Finance Subsidiary Company"},

        };

        public const int FinanceCompany = 4;
        public const int FinanceSubsidiaryCompany = 5;
    }
}
