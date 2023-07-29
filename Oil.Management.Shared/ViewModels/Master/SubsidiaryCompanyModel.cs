using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Shared.ViewModels.Master
{
    public class SubsidiaryCompanyModel: SubsidiaryCompanyAdd
    {
        public int IdSubsidiaryCompany { get; set; }
        public string StrStatus { get; set; }

        public string FactoryName { get; set; }
        public string BusinessName { get; set; }
    }

    public class SubsidiaryCompanyAdd
    {
        public int IdFactory { get; set; }
        public int IdBusniessUnit { get; set; }
        public string Name { get; set; }
        public string Nib { get; set; }
        public string Npwp { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }

        public string PicName { get; set; }

        public string PicPhone { get; set; }    

        public string PicEmail { get; set; }
    }
}
