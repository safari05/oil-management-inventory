using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Shared.Constants
{
    public class JenisAplikasiConstant
    {
        public Dictionary<int, string> DictJenisAplikasi = new Dictionary<int, string>()
        {
            {Web, "Aplikasi Web" },
            {Mobile, "Aplikasi MObile Include Android And IOS" },
        };
        public const int Web = 1;
        public const int Mobile = 2;
    }
}
