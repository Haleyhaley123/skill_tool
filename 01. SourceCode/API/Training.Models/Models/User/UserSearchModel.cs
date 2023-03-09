using NTSCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Models.Models.User
{
    public class UserSearchModel : SearchBaseModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public int? Status { get; set; }
    }
}
