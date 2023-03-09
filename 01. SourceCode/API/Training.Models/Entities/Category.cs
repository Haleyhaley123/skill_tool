using System;
using System.Collections.Generic;

#nullable disable

namespace Training.Models.Entities
{
    public partial class Category
    {
        public string CategoryId { get; set; }
        public string GroupCategoryId { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string TableName { get; set; }

        public virtual GroupCategory GroupCategory { get; set; }
    }
}
