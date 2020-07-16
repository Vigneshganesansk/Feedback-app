using System;
using System.Collections.Generic;

namespace Feedback_System.Entities
{
    public partial class Bumaster
    {
        public Bumaster()
        {
            EmployeeUserMaster = new HashSet<EmployeeUserMaster>();
        }

        public int Buid { get; set; }
        public string Buname { get; set; }
        public int? BuheadId { get; set; }

        public ICollection<EmployeeUserMaster> EmployeeUserMaster { get; set; }
    }
}
