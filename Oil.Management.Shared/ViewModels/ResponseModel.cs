using Oil.Management.Shared.ViewModels.ApplMgt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Shared.ViewModels
{
    public class ResponseModel <T>
    {
        public bool IsSuccess { get; set; }
        public string ReturnMessage { get; set; }
        public T Data { get; set; }

        public static implicit operator ResponseModel<T>(ResponseModel<List<ApplTaskModel>> v)
        {
            throw new NotImplementedException();
        }
    }
}
