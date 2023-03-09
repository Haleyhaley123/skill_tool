using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Models.Models.Combobox
{
    public class ComboboxModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string IdParent { get; set; }
        public string ObjectId { get; set; }
        public bool IsCheck { get; set; }
        public int Level { get; set; }
        public string IdLoaiDonVi { get; set; }
    }
}
