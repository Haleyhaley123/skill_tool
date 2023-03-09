using NTSCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Models.Models.Category
{
    public class CategorySearchModel : SearchBaseModel
    {
        public string Name { get; set; }
        public string TableName { get; set; }
        public string GroupCategoryId { get; set; }
        public string IdTinh { get; set; }
        public string IdHuyen { get; set; }
    }
}
