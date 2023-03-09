using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Models.Models.GroupCategory
{
    public class GroupCategoryCreateModel
    {
        public string GroupCategoryId { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
}
