using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Models.Models.Category
{
    public class CategoryExportModel
    {
        public string TableName { get; set; }
        public List<CategoryTableDataModel> ListCategory { get; set; }
        public CategoryExportModel()
        {
            ListCategory = new List<CategoryTableDataModel>();
        }
    }
}
