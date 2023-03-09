using NTS.Common.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Training.Models.Models.GroupUser
{
    public class GroupFunctionCreateModel
    {

        /// <summary>
        /// Tên nhóm quyền
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Trạng thái nhóm quyền
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// List quyền
        /// </summary>
        public List<PermissionModel> ListPermission { get; set; }

        public GroupFunctionCreateModel ()
        {
            ListPermission = new List<PermissionModel>();
        }
    }
}