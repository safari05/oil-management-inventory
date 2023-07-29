using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Shared.Constants
{
    public class StatusTransactionConstant
    {
        public static Dictionary<int, string> DictStatusTransaction = new Dictionary<int, string>
        {
            {Waiting,"Waiting" },
            {Pending,"Pending" },
            {Approve,"Approve" },
            {Reject ,"Reject"}
        };

        public const int Waiting = 0;
        public const int Pending = 1;
        public const int Approve = 2;
        public const int Reject = 3;
    }
}
