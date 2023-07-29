using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Shared.ViewModels.Reference
{
    public class BusinessUnitModel:BusinessUnitAddModel
    {
        public int IdBusinessUnit { get; set; }
        public string StrStatus { get; set; }

    }

    public class BusinessUnitAddModel
    {
        public string BusinessUnitName { get; set; }
        
    }
}
