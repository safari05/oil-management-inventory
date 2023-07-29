using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Shared.ViewModels
{
    public class ReqDataTableModel
    {
        public int PageStart { get; set; }
        public int RecordPerPage { get; set; }
    }

    public class RequestDatatableModel
    {
        public int PageStart { get; set; }
        public int RecordPerPage { get; set; }
        public string SortColumn { get; set; }
        public string SortColumnDirection { get; set; }
        public string SearchValue { get; set; }
    }

        
}
