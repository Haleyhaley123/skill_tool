using System;
using System.Collections.Generic;

#nullable disable

namespace Training.Models.Entities
{
    public partial class GroupCategory
    {
        public GroupCategory()
        {
            Category = new HashSet<Category>();
        }

        public string GroupCategoryId { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }

        public virtual ICollection<Category> Category { get; set; }
    }
}
