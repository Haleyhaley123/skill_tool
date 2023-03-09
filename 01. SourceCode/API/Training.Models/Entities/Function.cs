using System;
using System.Collections.Generic;

#nullable disable

namespace Training.Models.Entities
{
    public partial class Function
    {
        public Function()
        {
            GroupPermission = new HashSet<GroupPermission>();
            UserPermission = new HashSet<UserPermission>();
        }

        public string Id { get; set; }
        public string GroupFunctionId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public string ScreenCode { get; set; }

        public virtual GroupFunction GroupFunction { get; set; }
        public virtual ICollection<GroupPermission> GroupPermission { get; set; }
        public virtual ICollection<UserPermission> UserPermission { get; set; }
    }
}
