using System;
using System.Collections.Generic;

namespace Feedback_System.Entities
{
    public partial class ParticipationStatusMaster
    {
        public ParticipationStatusMaster()
        {
            Participants = new HashSet<Participants>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public ICollection<Participants> Participants { get; set; }
    }
}
