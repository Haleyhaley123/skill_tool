using System;
using System.Collections.Generic;

#nullable disable

namespace Training.Models.Entities
{
    public partial class UserPermission
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string FunctionId { get; set; }

        public virtual Function Function { get; set; }
        public virtual User User { get; set; }
    }
}
