using System;
using System.Collections.Generic;

#nullable disable

namespace Training.Models.Entities
{
    public partial class GroupFunction
    {
        public GroupFunction()
        {
            Function = new HashSet<Function>();
        }

        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }

        public virtual ICollection<Function> Function { get; set; }
    }
}
