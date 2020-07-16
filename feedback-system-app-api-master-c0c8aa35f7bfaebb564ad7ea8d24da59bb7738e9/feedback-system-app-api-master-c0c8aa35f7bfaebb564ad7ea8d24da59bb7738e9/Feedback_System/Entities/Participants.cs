using System;
using System.Collections.Generic;

namespace Feedback_System.Entities
{
    public partial class Participants
    {
        public Guid Guid { get; set; }
        public int AssociateId { get; set; }
        public string EventId { get; set; }
        public decimal VolenteerHours { get; set; }
        public decimal TravelHours { get; set; }
        public int ParticipationStatusId { get; set; }
        public int? EventRating { get; set; }
        public int RoleId { get; set; }
        public string Iiepcategory { get; set; }
        public bool? IsFeedbackSubmitted { get; set; }
        public string AssociateName { get; set; }
        public string Email { get; set; }

        public EmployeeUserMaster Associate { get; set; }
        public Events Event { get; set; }
        public ParticipationStatusMaster ParticipationStatus { get; set; }
        public RoleMaster Role { get; set; }
    }
}
