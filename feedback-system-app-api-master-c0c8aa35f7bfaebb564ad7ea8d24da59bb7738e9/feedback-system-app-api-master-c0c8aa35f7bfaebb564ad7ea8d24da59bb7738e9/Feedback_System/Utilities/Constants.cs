using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Utilities
{
    public class Constants
    {
        public static int Default_Int = 0;
        public static decimal Default_Decimal = 0.00M;

        public static string MonthFormat = "MMM";
        public static string DateFormat = "dd-MMM-yyyy";
        public static string LikeQuestion = "What did you like about this volunteering activity ?";
        public static string DislikeQuestion = "What can be improved in this volunteering activity ?";
        public static string NotParticipatedQuestion = "Hey there ! Please share your feedback for unregistering from the event.";
        public static string UnregisteredQuestion = "Hey there ! You have registered for the event. We would like to know the reason for not joining to understand if the team which created the event has some room for improvement in their process, so that we can get 100% participation from our registered attendees.";
        public static string Salt = "!Za3;ab72@";
        public static string DefaultPass = "pass@word1";

    }

}
