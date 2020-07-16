using System;
using System.Collections.Generic;

namespace Feedback_System.Entities
{
    public partial class Events
    {
        public Events()
        {
            Participants = new HashSet<Participants>();
        }

        public string EventId { get; set; }
        public string EventName { get; set; }
        public string Location { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }
        public string EventStatus { get; set; }
        public string CouncilName { get; set; }
        public int TotalVolenteers { get; set; }
        public decimal TotalVolenteerHours { get; set; }
        public decimal TotalTravelHours { get; set; }
        public decimal OverallVolenteerHours { get; set; }
        public int LivesImpacted { get; set; }
        public string BeneficiaryName { get; set; }
        public string Venue { get; set; }
        public int AddedUserId { get; set; }
        public DateTime AddedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int? DeletedUserId { get; set; }
        public DateTime? DeletedDate { get; set; }

        public ICollection<Participants> Participants { get; set; }
    }
}
