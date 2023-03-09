using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;

namespace Training.Models.Models.GroupUser
{
    public class GroupUserResultModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? Status { get; set; }
        public string Description { get; set; }
    }
}