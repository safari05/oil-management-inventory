using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Shared.ViewModels.Transaction
{
    public class TrxContractModel: TrxCntractAddModel
    {
        public int Id { get; set; }
        public string FileGuaranteStr { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string FactoryName { get; set; }
    }

    public class TrxCntractAddModel
    {
        public string Name { get; set; }
        public DateTime StartContract { get;set; }
        public DateTime EndContract { get;set; }    
        public IFormFile FileGuarante { get;set; }
        public int PctDomestic { get;set; }
        public int PctExport { get; set; }
        public string Description { get; set; } 
        public int IdFactory { get; set; }
    }
}
