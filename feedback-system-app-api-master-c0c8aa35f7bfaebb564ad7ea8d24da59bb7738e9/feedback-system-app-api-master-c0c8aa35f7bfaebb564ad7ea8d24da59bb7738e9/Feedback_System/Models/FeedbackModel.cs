using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Models
{
        public class FeedbackModel
        {
            public int ParticipantId { get; set; }
            public string EventId { get; set; }
            public string Question { get; set; }
            public string Answer { get; set; }
        }
}
