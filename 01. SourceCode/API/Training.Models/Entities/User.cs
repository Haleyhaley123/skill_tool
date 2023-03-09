using System;
using System.Collections.Generic;

#nullable disable

namespace Training.Models.Entities
{
    public partial class User
    {
        public User()
        {
            UserPermission = new HashSet<UserPermission>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string IdCanBo { get; set; }
        public string Anh { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public string GroupId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string IdDonVi { get; set; }

        public virtual ICollection<UserPermission> UserPermission { get; set; }
    }
}
