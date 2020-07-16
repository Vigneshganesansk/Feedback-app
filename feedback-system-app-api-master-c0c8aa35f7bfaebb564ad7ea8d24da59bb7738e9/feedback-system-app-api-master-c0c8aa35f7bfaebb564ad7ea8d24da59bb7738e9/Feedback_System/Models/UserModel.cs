using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Models
{
    public class UserModel
    {
        public int AssociateId { get; set; }
        public string AssociateName { get; set; }
        public string Email { get; set; }
        public int? RoleId { get; set; }
        public string Designation { get; set; }
        public int? Buid { get; set; }
        public int? AddedUserId { get; set; }
        public DateTime? AddedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int? DeletedUserId { get; set; }
        public string DeletedDate { get; set; }
    }
}
