using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Shared.ViewModels.Master
{
    public class FactoryModel:FactoryAddModel
    {
        public int IdFactory { get; set; }
        public string StrStatus { get; set; }

    }

    public class FactoryAddModel
    {
        public string FactoryName { get; set; }
        public string Nib { get; set; }
        public string Pic { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
       
    }
}
