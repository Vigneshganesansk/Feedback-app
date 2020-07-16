using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Utilities
{
    public class CodeValues
    {
        public enum ParticipationStatus
        {
            Participated = 1,
            NotPartcipated = 2,
            Unregistered = 3
        }

        public enum UserRole
        {
            Admin = 1,
            PMO = 2,
            POC = 3,
            Participant = 4
        }

    }
}
