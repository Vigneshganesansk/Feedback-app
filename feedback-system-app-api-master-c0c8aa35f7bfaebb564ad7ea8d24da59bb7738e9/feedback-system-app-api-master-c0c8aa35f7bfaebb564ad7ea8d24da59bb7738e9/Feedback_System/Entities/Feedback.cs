using System;
using System.Collections.Generic;

namespace Feedback_System.Entities
{
    public partial class Feedback
    {
        public Guid Guid { get; set; }
        public int AssociateId { get; set; }
        public string EventId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Qtype { get; set; }
    }
}
