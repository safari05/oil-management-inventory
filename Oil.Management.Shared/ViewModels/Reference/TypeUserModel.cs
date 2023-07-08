using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Shared.ViewModels.Reference
{
    public class TypeUserModel
    {
        public int IdTypeUser { get; set; }
        public string TypeName { get; set; }
    }

    public class TypeUserAddModel
    {
        public string TypeName { get; set; }
    }
}
