using System;
using System.Collections.Generic;
using System.Text;

namespace NTS.Common.Users
{
    public class NtsUserLoginModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string ImageLink { get; set; }
        public string UserId { get; set; }
        public int Status { get; set; }
        public string Password { get; set; }
        public string SecurityStamp { get; set; }
        public string PasswordHash { get; set; }
        public string Secret { get; set; }
        public int ExpireDateAfter { get; set; }
        public List<string> Permission { get; set; }
        public NtsUserLoginModel()
        {
            Permission = new List<string>();
        }
    }
}
