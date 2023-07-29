using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Shared.Constants
{
    public class JenisSatuanConstant
    {
        public static Dictionary<string, string> DictJenisSatuan = new Dictionary<string, string>()
        {
            {Liter,"Liter" },
            {Ton,"Ton" }
        };

        public const string Liter = "Liter";
        public const string Ton = "Ton";
    }
}
