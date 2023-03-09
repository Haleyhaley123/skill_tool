using Training.Models.Models.GroupFunction;
using System.Collections.Generic;

namespace Training.Models.Models.User
{
    public class UserCreateModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string IdCanBo { get; set; }
        public string IdChucVu { get; set; }
        public string IdCapBac { get; set; }
        public string FullName { get; set; }
        public string Anh { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageLink { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public int? Status { get; set; }
        public string Description { get; set; }
        public string GroupId { get; set; }
        public string IdDonVi { get; set; }

        /// <summary>
        /// Danh sách nhóm chức năng
        /// </summary>
        public List<GroupFunctionModel> ListGroupFunction { get; set; }
        public UserCreateModel()
        {
            ListGroupFunction = new List<GroupFunctionModel>();
        }
    }
}
