using System;
using System.Collections.Generic;

namespace Feedback_System.Entities
{
    public partial class Answers
    {
        public int AnsId { get; set; }
        public int QIdQuestion { get; set; }
        public string AnswerTextArea { get; set; }

        public Questions QIdQuestionNavigation { get; set; }
    }
}
