﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTS.Common.Models
{
    public class AppSettingModel
    {
        public string Secret { get; set; }
        public int ExpireDateAfter { get; set; }
        public string ServerFileUrl { get; set; }
        public string KeyAuthorize { get; set; }
        public string SubWebUrl { get; set; }
        public int Type { get; set; }
    }
}
