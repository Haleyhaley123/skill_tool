using NTS.Common.Resource;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Training.Models.Models.GroupUser
{
    public class GroupUserModel
    {
        public string Id { get; set; }

        [Required(ErrorMessageResourceName = MessageResourceKey.MSG0018, ErrorMessageResourceType = typeof(MessageResource))]
        [Display(Name = "Tên")]
        [MaxLength(300, ErrorMessageResourceName = MessageResourceKey.MSG0020, ErrorMessageResourceType = typeof(MessageResource))]
        public string Name { get; set; }
        public int? Status { get; set; }
        public string Description { get; set; }
        public List<PermissionModel> ListPermission { get; set; }
        public GroupUserModel()
        {
            ListPermission = new List<PermissionModel>();
        }
    }
}
