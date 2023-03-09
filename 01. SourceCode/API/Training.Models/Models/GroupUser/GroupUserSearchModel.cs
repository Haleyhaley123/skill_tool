using NTSCommon.Models;

namespace Training.Models.Models.GroupUser
{
    public class GroupUserSearchModel : SearchBaseModel
    {
        public string Name { get; set; }
        public int? Status { get; set; }
    }
}