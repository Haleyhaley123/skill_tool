using System;
using System.Collections.Generic;

#nullable disable

namespace Training.Models.Entities
{
    public partial class GroupPermission
    {
        public string Id { get; set; }
        public string GroupId { get; set; }
        public string FunctionId { get; set; }

        public virtual Function Function { get; set; }
    }
}
