using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Shared.Settings
{
    public class AppMgtSettingModel
    {
        public string Secret { get; set; }
        public string ConnectionString { get; set; }
        public string PathFile { get; set; }
    }
}
