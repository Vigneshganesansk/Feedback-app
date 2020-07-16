using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Models
{
    public class EventsModel
    {
        public string EventID { get; set; }
        public string EventName { get; set; }
        public string EventDate { get; set; }
        public string EventDescription { get; set; }
        public string BaseLocation { get; set; }
        public string CouncilName { get; set; }
        public string BeneficiaryName { get; set; }
        public string Venue { get; set; }
        public string EventStatus { get; set; }
        public int TotalVolenteers { get; set; }
        public decimal TotalVolenteerHours { get; set; }
        public decimal TotalTravelHours { get; set; }
        public decimal OverallVolenteerHours { get; set; }
        public int LivesImpacted { get; set; }
        public string Month { get; set; }
    }
}
