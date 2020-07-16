using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Models
{
    public class ParticipantsModel
    {
        public Guid ParticipantGuid { get; set; }
        public string EventId { get; set; }
        public int AssociateId { get; set; }
        public string AssociateName { get; set; }
        public string Email { get; set; }
        public int ParticipationStatusId { get; set; }
        public string ParticipationStatusName { get; set; }
        public decimal VolenteerHours { get; set; }
        public decimal TravelHours { get; set; }
        public string IIEPCategory { get; set; }
        public int? EventRating { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Likes { get; set; }
        public string Dislikes { get; set; }
        public bool IsFeedbackSubmitted { get; set; }
    }

}
