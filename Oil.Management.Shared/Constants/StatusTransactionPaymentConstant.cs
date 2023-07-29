using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Shared.Constants
{
    public class StatusTransactionPaymentConstant
    {
        public static Dictionary<int, string> StatusTransactionPayment = new Dictionary<int, string>()
        {
            { Initiated,"Initiated"},
            { Unpaid,"Unpaid"},
            { Paid,"Paid"},
        };

        public const int Initiated = 0;
        public const int Unpaid = 1;
        public const int Paid = 2;
    }
}
