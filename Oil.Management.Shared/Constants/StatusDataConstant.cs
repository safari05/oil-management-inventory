﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Shared.Constants
{
    public class StatusDataConstant
    {
        public static Dictionary<int, string> DictStatusData = new Dictionary<int, string>
        {
            {Pending,"Pending" },
            {Aktif,"Aktif" },
            {NoAktif,"No Aktif" }
        };

        public const int Pending = 0;
        public const int Aktif = 1;
        public const int NoAktif = -1;
    }
}
