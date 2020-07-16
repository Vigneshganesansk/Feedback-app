using System;
using System.Collections.Generic;

namespace Feedback_System.Entities
{
    public partial class EmployeeUserMaster
    {
        public EmployeeUserMaster()
        {
            Participants = new HashSet<Participants>();
        }

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
        public string Password { get; set; }
        public string Salt { get; set; }

        public Bumaster Bu { get; set; }
        public RoleMaster Role { get; set; }
        public ICollection<Participants> Participants { get; set; }
    }
}
