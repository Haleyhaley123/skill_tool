using NTSCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Models.Models.UserHistory
{
    public class UserHistorySearchModel : SearchBaseModel
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public int? Type { get; set; }
        public bool IsExport { get; set; }
    }
}
