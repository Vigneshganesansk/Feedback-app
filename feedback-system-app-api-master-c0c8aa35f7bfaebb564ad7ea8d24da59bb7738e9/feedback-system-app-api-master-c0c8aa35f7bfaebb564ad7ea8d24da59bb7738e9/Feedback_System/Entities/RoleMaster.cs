using System;
using System.Collections.Generic;

namespace Feedback_System.Entities
{
    public partial class RoleMaster
    {
        public RoleMaster()
        {
            EmployeeUserMaster = new HashSet<EmployeeUserMaster>();
            Participants = new HashSet<Participants>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public ICollection<EmployeeUserMaster> EmployeeUserMaster { get; set; }
        public ICollection<Participants> Participants { get; set; }
    }
}
