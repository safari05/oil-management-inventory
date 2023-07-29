using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Shared.ViewModels.Master
{
    public class CustomerModel: CustomerAddModel
    {
        public int IdCustomer { get; set; }
        public string NameSubsidiary { get; set; }
        public string NameVillage { get; set; }
        public string StrStatus { get; set; }

    }

    public class CustomerAddModel
    {
        public string CutomerName { get; set; }
        public string Nib{ get; set; }
        public string Npwp{ get; set; }
        public string Phone{ get; set; }
        public string Email{ get; set; }
        public string Website{ get; set; }
        public string Address{ get; set; }
        public string PicName{ get; set; }
        public string PicPhone{ get; set; }
        public string PicEmail{ get; set; }
        public int IdSubsidiaryCompany{ get; set; }
        public int IdVillage{ get; set; }

    }
}
