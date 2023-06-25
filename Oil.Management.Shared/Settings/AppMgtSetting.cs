using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Shared.Settings
{
    public class AppMgtSetting
    {
        //from appsetting.json
        public static string Secret { get; set; }
        public static string ConnectionString { get; set; }
    }
}
