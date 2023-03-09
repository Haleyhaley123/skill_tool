using System;
using System.Collections.Generic;

#nullable disable

namespace Training.Models.Entities
{
    public partial class UserHistory
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string ClientIP { get; set; }
        public string OS { get; set; }
        public string BrowserVersion { get; set; }
        public string BrowserName { get; set; }
        public string Device { get; set; }
        public DateTime CreateDate { get; set; }
        public int Type { get; set; }
    }
}
